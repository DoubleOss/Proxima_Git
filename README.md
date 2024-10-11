# 졸업작품 전시회 게임 Proxima
* * *
## 📝│개요
> ### 장르: TPS 슈팅 게임 
>
> ### 제작기간 2024.09.09 ~ 2024.09.26
> ### 사용엔진: Unity3D 2022.03.22f1 URP Project
> ### 사용기술: UGUI, C#, Visual Studio


> ## 💬│게임 간략 영상
> ![Video Label](http://img.youtube.com/vi/v7we92hQYAI/0.jpg)
>
> (https://youtu.be/v7we92hQYAI)

***

## 👨🏻‍💻│기능 구현 목차
###   1. [TPS 카메라](https://github.com/DoubleOss/GroundWorld_Open?tab=readme-ov-file#1-%EB%A7%88%EC%9D%B8%ED%81%AC%EB%9E%98%ED%94%84%ED%8A%B8-%EB%8F%99%EC%98%81%EC%83%81-%EB%9D%BC%EC%9D%B4%EB%B8%8C%EB%9F%AC%EB%A6%AC-%EC%97%B0%EA%B2%B0)
###   2. [Player Controll](https://github.com/DoubleOss/GroundWorld_Open?tab=readme-ov-file#2-%EB%A7%88%EC%9D%B8%ED%81%AC%EB%9E%98%ED%94%84%ED%8A%B8-tinysound-lib-%EC%97%B0%EB%8F%99)
###   3. [Weapon Chanage](https://github.com/DoubleOss/GroundWorld_Open?tab=readme-ov-file#3-hud-%EC%8B%9C%EC%8A%A4%ED%85%9C-1)
###   4. [UI Animation](https://github.com/DoubleOss/GroundWorld_Open?tab=readme-ov-file#4-%EC%83%81%EC%A0%90-%EC%8B%9C%EC%8A%A4%ED%85%9C-1)
###   5. [Boss AI](https://github.com/DoubleOss/GroundWorld_Open?tab=readme-ov-file#5-%EC%8A%A4%EB%A7%88%ED%8A%B8%ED%8F%B0-%EC%8B%9C%EC%8A%A4%ED%85%9C-1)


### 1. TPS 카메라 구현
> * ### 1인칭 시점이 아닌 3인칭 시점 구현
> * ### 카메라 앞에 오브젝트가 있을 경우 카메라 보정
> ![2024-10-12 02;34;42](https://github.com/user-attachments/assets/6c9d3dd6-7b35-470b-95dc-ab6abe15a46b)
> ## 🔗 코드 링크
> * ### [TPS Camera](https://github.com/DoubleOss/GroundWorld_Open/blob/main/src/main/java/com/doubleos/gw/proxy/ClientProxy.java#L614)

***

### 2. Player 컨트롤 구현
> * ### Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") 을 이용한 이동 구현
> * ### CharacterController 이용한 구현 
> ![2024-10-12 02;47;36](https://github.com/user-attachments/assets/2b99302d-bffe-4d29-833b-686ba6637476)
> ## 🔗 코드 링크
> * ### [Move Func](https://github.com/DoubleOss/GroundWorld_Open/blob/main/src/main/java/com/doubleos/gw/proxy/ClientProxy.java#L614)


### 3. 무장 변경 기능
> * ### 각 무장에 맞는 애니메이션 레이어 변경
> * ### HUD와 연계
> ![2024-10-12 02;56;49](https://github.com/user-attachments/assets/bf0c5d56-80aa-451a-97fc-f79b17a1ce9c)
> ## 🔗 코드 링크
> * ### [Weapon Chanage](https://github.com/DoubleOss/GroundWorld_Open/blob/main/src/main/java/com/doubleos/gw/proxy/ClientProxy.java#L614)

### 4. UI Animation
> * ### GraphicRaycaster, PointerEventData 통한 UI Object 인식
> * ### Tween Scale 사용
> ![2024-10-12 03;08;25](https://github.com/user-attachments/assets/ee314a47-3a64-42d5-8aa5-45ab482c99c9)
> ## 🔗 코드 링크
> * ### [Menu UI Animation](https://github.com/DoubleOss/GroundWorld_Open/blob/main/src/main/java/com/doubleos/gw/proxy/ClientProxy.java#L614)


### 5. Boss AI
> * ### 일정 패턴 사용후 그로기 패턴
> * ### 사전 패턴 예고
> * ### 보스 히트박스 구현
>![2024-10-12 03;17;06](https://github.com/user-attachments/assets/23a4b681-9afd-482f-871e-33a4b8f7d5d9)
>![2024-10-12 03;18;00](https://github.com/user-attachments/assets/3f39ba90-038e-44d1-b5f1-14263c128b50)
> ## 🔗 코드 링크
> * ### [Boss AI](https://github.com/DoubleOss/GroundWorld_Open/blob/main/src/main/java/com/doubleos/gw/proxy/ClientProxy.java#L614)




