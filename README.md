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
###   1. [TPS 카메라](https://github.com/DoubleOss/Proxima_Git?tab=readme-ov-file#1-tps-%EC%B9%B4%EB%A9%94%EB%9D%BC-%EA%B5%AC%ED%98%84)
###   2. [Player Controll](https://github.com/DoubleOss/Proxima_Git?tab=readme-ov-file#2-player-%EC%BB%A8%ED%8A%B8%EB%A1%A4-%EA%B5%AC%ED%98%84)
###   3. [Weapon Chanage](https://github.com/DoubleOss/Proxima_Git?tab=readme-ov-file#3-%EB%AC%B4%EC%9E%A5-%EB%B3%80%EA%B2%BD-%EA%B8%B0%EB%8A%A5)
###   4. [UI Animation](https://github.com/DoubleOss/Proxima_Git?tab=readme-ov-file#4-ui-animation-1)
###   5. [Boss AI](https://github.com/DoubleOss/Proxima_Git?tab=readme-ov-file#5-boss-ai-1)


### 1. TPS 카메라 구현
> * ### 1인칭 시점이 아닌 3인칭 시점 구현
> * ### 카메라 앞에 오브젝트가 있을 경우 카메라 보정
> ![2024-10-12 02;34;42](https://github.com/user-attachments/assets/6c9d3dd6-7b35-470b-95dc-ab6abe15a46b)
> ## 🔗 코드 링크
> * ### [TPS Camera](https://github.com/DoubleOss/Proxima_Git/blob/main/Scripts/Player/TpsCamera.cs)

***

### 2. Player 컨트롤 구현
> * ### Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") 을 이용한 이동 구현
> * ### CharacterController 이용한 구현 
> ![2024-10-12 02;47;36](https://github.com/user-attachments/assets/2b99302d-bffe-4d29-833b-686ba6637476)
> ## 🔗 코드 링크
> * ### [Move Func](https://github.com/DoubleOss/Proxima_Git/blob/main/Scripts/Player/PlayerControll.cs#L735)


### 3. 무장 변경 기능
> * ### 각 무장에 맞는 애니메이션 레이어 변경
> * ### HUD와 연계
> ![2024-10-12 02;56;49](https://github.com/user-attachments/assets/bf0c5d56-80aa-451a-97fc-f79b17a1ce9c)
> ## 🔗 코드 링크
> * ### [Weapon Chanage](https://github.com/DoubleOss/Proxima_Git/blob/main/Scripts/Player/PlayerControll.cs#L354)

### 4. UI Animation
> * ### GraphicRaycaster, PointerEventData 통한 UI Object 인식
> * ### Tween Scale 사용
> ![2024-10-12 03;08;25](https://github.com/user-attachments/assets/ee314a47-3a64-42d5-8aa5-45ab482c99c9)
> ## 🔗 코드 링크
> * ### [Menu UI Animation](https://github.com/DoubleOss/Proxima_Git/blob/main/Scripts/UI/MenuUIControll.cs#L119)


### 5. Boss AI
> * ### 일정 패턴 사용후 그로기 패턴
> * ### 사전 패턴 예고
> * ### 보스 히트박스 구현
>![2024-10-12 03;17;06](https://github.com/user-attachments/assets/23a4b681-9afd-482f-871e-33a4b8f7d5d9)
>![2024-10-12 03;18;00](https://github.com/user-attachments/assets/3f39ba90-038e-44d1-b5f1-14263c128b50)
> ## 🔗 코드 링크
> * ### [Boss AI](https://github.com/DoubleOss/Proxima_Git/blob/main/Scripts/Monster/BossMonster.cs#L189)




