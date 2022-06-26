# CustomAPKBuilder

### 개요

살아있는 동화 (K-Project) 에서 사용하던 Custom Build Module을 Upgrade 및 모듈화 진행

![image](https://user-images.githubusercontent.com/43736049/175814682-75d2d56f-74ec-4d4f-9c1f-81162db48e6b.png)  
•	살아있는 동화에서 사용하던 Build Window

### 목적

•	개발자 외에 다른인력(기획자, 서브 개발자 등)도 이슈 없이 Build Setting을 공유하고, Build 할 수 있는 환경을 지원

### 목표

•	어느 프로젝트에서나 사용할 수 있도록 독립적인 모듈
(다른 모듈 혹은 Package 기능과 Dependency 최소화)  
•	여러 프로젝트 사용에 불편함이 없도록, 빌드 시에 필요한 기능들을 최대한 포함                                        
•	초기 Setting을 적용할 경우 해당 Setting을 고정 및 다른 사용자와 Setting을 공유 할 수 있는 환경 제공

### 설치 방법
•	Package Manager에 Git repo 주소를 입력하여 패키지를 등록
(https://github.com/MorphYoungBinKim/CustomAPKPackage.git)
 ![image](https://user-images.githubusercontent.com/43736049/175814772-109d1408-09e5-4d45-99e2-d01719f14b11.png)

### Build Window
•	상단 메뉴의 Build -  Build APK 탭을 선택하여 호출

![image](https://user-images.githubusercontent.com/43736049/175816085-473e7d04-901c-498d-b746-d0bdd2bf0af4.png)

1.	상단 Tab  
•	Save – Editor 폴더에 전체 Data가 담겨 있는 ScriptableObject를 생성  
•	Load – 외부에 있는 AutoBuildDataObject를 받아와서 데이터를 프로젝트에 적용  
•	Export – 현재 상태를 AutoBuildDataObject로 생성하여 외부에 저장  

2.	APK Info  
•	APKName – Build 될 APK의 이름 지정  
•	APK Path – APK가 저장 될 경로 지정  
•	APP Version – APP Version 지정  
•	Version Code – Version Code 지정  
•	Build Target – Build 할 APK의 Target 지정 (Android / iOS)  
•	Build Type – Build 할 서버의 Type를 지정 (Product / Stage)  

3.	Other Setting
•	Scene Tab – Build Setting에 있는 Scene In Build 창과 동일한 구성  
Build될 Scene을 설정하여 포함  
•	Event Tab – ServerType 및 Build 상태에 따른 Event 설정 가능  
(Build 전, 후에 호출되는 Event)  
•	Custom Setting Tab – Company Name , Product Name , SDK Version 등을  
설정 할 수 있음 (Schema 및 KeyStore Update 예정)  

