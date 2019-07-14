#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>

const char* ssid = "GVS";
const char* password = "0987654321";

const char* host = "maker.ifttt.com";
const int httpsPort = 443;
int count=0;
int outputpin = A0;
int max_limit=33;
int analogValue=analogRead(outputpin);
 
WiFiClientSecure client;

void setup() 
{
  Serial.begin(115200);
  Serial.println();
  Serial.print("connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  WiFiClientSecure client;
  Serial.print("connecting to ");
  Serial.println(host);
  if (!client.connect(host, httpsPort)) 
  {
    Serial.println("connection failed");
    return;
  }
float millivolts = (analogValue/1024.0) * 3300; //3300 is the voltage provided by NodeMCU
float celsius = millivolts/10;
Serial.println("Sensor value:");
Serial.print(celsius);
Serial.println();
 
}

void loop() 
{
if(count==0)
{
  if(analogValue>max_limit)
  {
   String url = "/trigger/ESP/with/key/omxQjR1SKwPh0pn9r7-VLLoDUBzfwKGKWddYD_95pji";
  Serial.print("requesting URL: ");
  Serial.println(url);

  client.print(String("GET ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" +
               "User-Agent: BuildFailureDetectorESP8266\r\n" +
               "Connection: close\r\n\r\n");

  Serial.println("request sent");
  while (client.connected()) 
  {
    String line = client.readStringUntil('\n');
    if (line == "\r") 
    {
      Serial.println("headers received");
      break;
    }
  }
  String line = client.readStringUntil('\n');

  Serial.println("reply was:");
  Serial.println("==========");
  Serial.println(line);
  Serial.println("==========");
  Serial.println("closing connection");
    count++;
  }
}
}
