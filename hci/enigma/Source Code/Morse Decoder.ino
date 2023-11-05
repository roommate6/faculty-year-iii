#include <LiquidCrystal.h>

class Node {
private:
  char character;
  Node* leftChild;
  Node* rightChild;
public:

  Node(char character, Node* leftChild, Node* rightChild) {
    this->character = character;
    this->leftChild = leftChild;
    this->rightChild = rightChild;
  }
  Node(char character) {
    this->character = character;
    this->leftChild = NULL;
    this->rightChild = NULL;
  }

  char decodeMorse(Node* root, String morseCode) {
    if (morseCode.length() == 0) {
      return root->character;
    } else if (morseCode[0] == '.') {
      return decodeMorse(root->leftChild, morseCode.substring(1));
    } else if (morseCode[0] == '-') {
      return decodeMorse(root->rightChild, morseCode.substring(1));
    }
  }
};

///////////////////////////////////////////////////
// Arborele binar utilizat pentru decodare Morse //
///////////////////////////////////////////////////

Node* morseTreeRoot;

//////////////////////////////////////////////////
// Pini si variabile utilizate de afisajul LCD //
//////////////////////////////////////////////////

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2, audioInPin = A0;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

const int columns = 16;
const int rows = 2;

int lcdIndex = 0;
int line1[columns];
int line2[columns];

////////////////////////
// Variabile de stare //
////////////////////////

int realState = LOW;
int realStatePrevious = LOW;
int filteredState = LOW;
int filteredStatePrevious = LOW;

//////////////////////////////////////////
// Variabile pentru algoritmul Goertzel //
//////////////////////////////////////////

int toneLimit = 100;
int toneLowerLimit = 100;

float coeff;
float Q1 = 0;
float Q2 = 0;
float sine;
float cosine;
float samplingFreq = 8928.0;
float targetFreq = 558.0;

////////////////////////////////////////////////////////
// Numarul de mostre pentru a calcula latimea de unda //
////////////////////////////////////////////////////////

float n = 50.0;
int testData[50];

/////////////////////////////////////////////////////
// O marja de eroare in trecerea de la HIGH la LOW //
/////////////////////////////////////////////////////

int marginTime = 6;

/////////////////////////////////////////////////////
// Variabile masurare durate ale semnalului primit //
/////////////////////////////////////////////////////

long startTimeHigh;
long highDuration;
long lastHighDuration;
long highTimeAverage;
long lowTimeAverage;
long startTimeLow;
long lowDuration;
long lastStartTime = 0;

char code[20];
int stop = LOW;
int wpm;

void setup() {
  morseTreeRoot =
    new Node('@',
             new Node('E',
                      new Node('I',
                               new Node('S',
                                        new Node('H',
                                                 new Node('5'),
                                                 new Node('4')),
                                        new Node('V',
                                                 new Node('@'),
                                                 new Node('3'))),
                               new Node('U',
                                        new Node('F',
                                                 new Node('@'),
                                                 new Node('@')),
                                        new Node('@',
                                                 new Node('@'),
                                                 new Node('2')))),
                      new Node('A',
                               new Node('R',
                                        new Node('L',
                                                 new Node('@'),
                                                 new Node('@')),
                                        new Node('@',
                                                 new Node('+'),
                                                 new Node('@'))),
                               new Node('W',
                                        new Node('P',
                                                 new Node('@'),
                                                 new Node('@')),
                                        new Node('J',
                                                 new Node('@'),
                                                 new Node('1'))))),
             new Node('T',
                      new Node('N',
                               new Node('D',
                                        new Node('B',
                                                 new Node('6'),
                                                 new Node('=')),
                                        new Node('X',
                                                 new Node('/'),
                                                 new Node('@'))),
                               new Node('K',
                                        new Node('C',
                                                 new Node('@'),
                                                 new Node('@')),
                                        new Node('Y',
                                                 new Node('@'),
                                                 new Node('@')))),
                      new Node('M',
                               new Node('G',
                                        new Node('Z',
                                                 new Node('7'),
                                                 new Node('@')),
                                        new Node('Q',
                                                 new Node('@'),
                                                 new Node('@'))),
                               new Node('O',
                                        new Node('@',
                                                 new Node('8'),
                                                 new Node('@')),
                                        new Node('@',
                                                 new Node('9'),
                                                 new Node('0'))))));

  //////////////////////////////////////
  // Initializare algoritmul Goertzel //
  //////////////////////////////////////
  int k;
  float omega;
  k = (int)(0.5 + ((n * targetFreq) / samplingFreq));
  omega = (2.0 * PI * k) / n;
  sine = sin(omega);
  cosine = cos(omega);
  coeff = 2.0 * cosine;

  Serial.begin(115200);

  lcd.begin(columns, rows);

}

void loop() {
  //////////////////////////////////////////////////////////////////////////
  // Calculul prin care obtinem tonul, pentru numarul de mostre selectate //
  //////////////////////////////////////////////////////////////////////////
  float tone;


  for (char index = 0; index < n; index++) {
    testData[index] = analogRead(audioInPin);
  }
  for (char index = 0; index < n; index++) {
    float Q0;
    Q0 = coeff * Q1 - Q2 + (float)testData[index];
    Q2 = Q1;
    Q1 = Q0;
  }
  float toneSquared = (Q1 * Q1) + (Q2 * Q2) - Q1 * Q2 * coeff;  // utilizam doar rezultatul real al algoritmului
  tone = sqrt(toneSquared);
  Q2 = 0;
  Q1 = 0;

  /////////////////////
  // Setarea tonului //
  /////////////////////

  if (tone > toneLowerLimit) {
    toneLimit = (toneLimit + ((tone - toneLimit) / 5));
  }

  if (toneLimit < toneLowerLimit) {
    toneLimit = toneLowerLimit;
  }

  /////////////////////////////
  // Verificam tonul obtinut //
  /////////////////////////////

  if (tone > toneLimit / 2) {
    realState = HIGH;
  } else {
    realState = LOW;
  }

  /////////////////////////////////////////////////////
  // Aplicam o marja de timp la trecerea intre stari //
  /////////////////////////////////////////////////////

  if (realState != realStatePrevious) {
    lastStartTime = millis();
  }
  if ((millis() - lastStartTime) > marginTime) {
    if (realState != filteredState) {
      filteredState = realState;
    }
  }

  ///////////////////////////////////////////////////////////////////
  //               Calculam durata a HIGH si LOW                   //
  // Consideram durata medie a HIGH ca durata unei unitati (punct) //
  ///////////////////////////////////////////////////////////////////

  if (filteredState != filteredStatePrevious) {
    if (filteredState == HIGH) {
      startTimeHigh = millis();
      lowDuration = (millis() - startTimeLow);
    }

    if (filteredState == LOW) {
      startTimeLow = millis();
      highDuration = (millis() - startTimeHigh);

      if (highDuration < (2 * highTimeAverage) || highTimeAverage == 0) {
        highTimeAverage = (highDuration + 2 * highTimeAverage) / 3;
      }
      if (highDuration > (5 * highTimeAverage)) {
        highTimeAverage = highDuration + highTimeAverage;
      }
    }
  }

  //////////////////////////////////////////////
  // Analizam semnalul primit si setam WPM-ul //
  //////////////////////////////////////////////

  if (filteredState != filteredStatePrevious) {
    stop = LOW;
    if (filteredState == LOW) {
      if (highDuration < (highTimeAverage * 2) && highDuration > (highTimeAverage / 2)) {  // punct
        strcat(code, ".");
         wpm = (wpm + (1200 / ((highDuration)))) / 2;
      }
      if (highDuration > (highTimeAverage * 2) && highDuration < (highTimeAverage * 6)) {  // linie
        strcat(code, "-");
      }
    }

    if (filteredState == HIGH) {
      if (lowDuration > (highTimeAverage * 2) && lowDuration < (highTimeAverage * 5))  // spatiu intre caractere
      {
        decode();
        code[0] = '\0';
        Serial.print("/");
      }
      if (lowDuration >= (highTimeAverage * 5))  // spatiu intre cuvinte
      {
        decode();
        code[0] = '\0';
        printAscii(32);
        Serial.println();
      }
    }
  }

  if ((millis() - startTimeLow) > (highDuration * 6) && stop == LOW) {  // capatul input-ului
    decode();
    code[0] = '\0';
    stop = HIGH;
  }

  updateInfoLineLcd();
  realStatePrevious = realState;
  lastHighDuration = highDuration;
  filteredStatePrevious = filteredState;
}

///////////////////////////////////
// Transforma cod morse in ASCII //
///////////////////////////////////

void decode() {
  int result = (int)morseTreeRoot->decodeMorse(morseTreeRoot, code);
  if (result != 64) {
    printAscii(result);
  }
}

/////////////////////////////
// Afisare caracter pe LCD //
/////////////////////////////

void printAscii(int asciiNumber) {
  if (lcdIndex > columns - 1) {
    lcdIndex = 0;
    for (int i = 0; i <= columns - 1; i++) {
      lcd.setCursor(i, rows - 2);
      lcd.write(line1[i]);
      lcd.setCursor(i, rows - 1);
      lcd.write(32);
    }
  }

  line1[lcdIndex] = asciiNumber;
  lcd.setCursor(lcdIndex, rows - 1);
  lcd.write(asciiNumber);
  lcdIndex++;
}

///////////////////////////////////
// Actualizare WPF afisat pe LCD //
///////////////////////////////////

void updateInfoLineLcd() {
  if (wpm < 10) {
    lcd.setCursor(0, 0);
    lcd.print("0");
    lcd.setCursor(1, 0);
    lcd.print(wpm);
    lcd.setCursor(2, 0);
    lcd.print(" WPM");
  } else {
    lcd.setCursor(0, 0);
    lcd.print(wpm);
    lcd.setCursor(2, 0);
    lcd.print(" WPM ");
  }
}