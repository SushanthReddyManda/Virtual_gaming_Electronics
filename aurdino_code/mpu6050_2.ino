/* ARDUINO CODE.   Copy and paste in Arduino IDE
 
// MPU 6050   --->   Arduino
//
//  VCC   ----->   3.3v
//  GND   ----->   GND
//  SCL   ----->   A5 pin
//  SDA   ----->   A4 pin*/
 
 
#include<Wire.h>
const int MPU=0x68;  // I2C address of the MPU-6050
int16_t AcX,AcY,AcZ,Tmp,GyX,GyY,GyZ;
 const int flexPin1  = A0; //pin A0 to read analog input
const int flexPin2  = A1;// for long flex sensor 120,220 oorg,for short flex sensor 40,140 yvrg
//Variables:
int value1; //save analog value
int value2;
 
// value returned is in interval [-32768, 32767] so for normalize multiply GyX and others for gyro_normalizer_factor
// float gyro_normalizer_factor = 1.0f / 32768.0f;
 
void setup(){
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B);  // PWR_MGMT_1 register
  Wire.write(0);     // set to zero (wakes up the MPU-6050)
  Wire.endTransmission(true);

  Serial.begin(9600);
}
 
 
void loop(){

  
  value1 = analogRead(flexPin1);         //Read and save analog value from potentiometer
  value2 = analogRead(flexPin2);  
  /*//Print value
  if(value<750)Serial.println("0");
  if(value>750)Serial.print("1");
  delay(10);*/                          //Small delay
  
  Wire.beginTransmission(MPU);
  Wire.write(0x3B);  // starting with register 0x3B (ACCEL_XOUT_H)
  Wire.endTransmission(false);
  Wire.requestFrom(MPU,14,true);  // request a total of 14 registers
  AcX=Wire.read()<<8|Wire.read();  // 0x3B (ACCEL_XOUT_H) & 0x3C (ACCEL_XOUT_L)    
  AcY=Wire.read()<<8|Wire.read();  // 0x3D (ACCEL_YOUT_H) & 0x3E (ACCEL_YOUT_L)
  AcZ=Wire.read()<<8|Wire.read();  // 0x3F (ACCEL_ZOUT_H) & 0x40 (ACCEL_ZOUT_L)
  Tmp=Wire.read()<<8|Wire.read();  // 0x41 (TEMP_OUT_H) & 0x42 (TEMP_OUT_L)
  GyX=Wire.read()<<8|Wire.read();  // 0x43 (GYRO_XOUT_H) & 0x44 (GYRO_XOUT_L)
  GyY=Wire.read()<<8|Wire.read();  // 0x45 (GYRO_YOUT_H) & 0x46 (GYRO_YOUT_L)
  GyZ=Wire.read()<<8|Wire.read();  // 0x47 (GYRO_ZOUT_H) & 0x48 (GYRO_ZOUT_L)

  Serial.print(AcX); Serial.print(";"); Serial.print(AcY); Serial.print(";"); Serial.print(AcZ); Serial.print(";");
  Serial.print(GyX); Serial.print(";"); Serial.print(GyY); Serial.print(";"); Serial.print(GyZ);Serial.print(";");
  Serial.print(value1); Serial.print(";");Serial.print(value2);Serial.println("");
  Serial.flush();
 
  delay(10);
}


 
