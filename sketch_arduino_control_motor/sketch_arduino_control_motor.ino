#include <Servo.h>
Servo myServo;
Servo myServo1;

unsigned long last_time = 0;
String comStr = "";
int comInt=0;
String force;
float stimulus;
float pwm;

void setup() {
  // put your setup code here, to r  un once:
  Serial.begin(9600); // start serial port
  while(Serial.read()>=0){} // clear serial port's buffer

  
  myServo.attach(9); // attach myServo to GPIO_09
  myServo.writeMicroseconds(1000); // set initial PWM to minimal
  myServo1.attach(10); // attach myServo to GPIO_09
  myServo1.writeMicroseconds(1000); // set initial PWM to minimal
}

void loop() {
  if (millis() > last_time + 2000)
    {
        //Serial.println("Arduino is alive!!");
        last_time = millis();
    }

   
  // put your main code here, to run repeatedly:
  if (Serial.available() > 0){
    // listen the Serial port, run the code when something catched..
    
    force = Serial.readString();
    Serial.println(force);
    stimulus = force.toFloat();
    //pwm = (abs(stimulus)+2.8308)/0.0027;
    //myServo.writeMicroseconds(stimulus);
      
    if(stimulus >= 0 && stimulus < 10){
      pwm = (abs(stimulus)+2.628)/0.0025;
      pwm = constrain(pwm,1000,2000);
      Serial.println(pwm);
      myServo1.writeMicroseconds(pwm);
      myServo.writeMicroseconds(1050);
    }
    else if(stimulus < 0){
      pwm = (abs(stimulus)+2.8308)/0.0027;
      pwm = constrain(pwm,1000,2000);
      Serial.println(pwm);
      myServo.writeMicroseconds(pwm);
      myServo1.writeMicroseconds(1050);
    }
    else{
      myServo.writeMicroseconds(1000);
      myServo1.writeMicroseconds(1000);
    }
    
    
//    delay(10);
//    comStr = Serial.readString(); // read out the string
//    comInt = comStr.toInt(); // convert the string to integer
//    comInt = constrain(comInt,1000,2000); // limit the integer between to 1000 and 2000
//
//    Serial.println(comInt); // show the integer number on Serial Monitor
//    myServo.writeMicroseconds(comInt); // write the integer number to Servo in unit of micro-second
//    myServo1.writeMicroseconds(comInt); // write the integer number to Servo in unit of micro-second
//
//    delay(5000); //delay 5s
//    Serial.println(1000);
//    myServo.writeMicroseconds(1000); // write the integer number to Servo in unit of micro-second
//    myServo1.writeMicroseconds(1000); // write the integer number to Servo in unit of micro-second
  }
}
