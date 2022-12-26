# 2022 송년회 가챠 시뮬레이터
<div>
    <h2> 앱 정보 </h2>
    <img src = "https://img.itch.zone/aW1nLzEwODcxMDgyLnBuZw==/347x500/beyqbB.png"><br>
    <img src="https://img.shields.io/badge/Unity-yellow?style=flat-square&logo=Unity&logoColor=FFFFFF"/>
    <h4> 개발 일자 : 2022.12 <br><br>
    다운로드 : https://drive.google.com/file/d/1JjjZcmYqqWitjGHhUYGCUntiGlppijMc/view?usp=sharing
    
  </div>
  <div>
    <h3> 앱 설명 </h3>
     2022년 <br><br>
    <h2> 사용 방식 </h2>
    <h3> 기본 사용 </h3>
     Assets 폴더 안에 GiftInfo 폴더 안에 <br><br>
     20초 동안 눈이 감기지 않도록 버티는 게임이다.<br><br>
  </div> 
  <div>
    <h2> 어플 스크린샷 </h2>
      <table>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMjZfNzkg/MDAxNjcyMDE5OTg5MDIx.uD1cvzt0BMkvTh83CReXBZ4tX7hnlFXADMmlC2N1Ofsg.7yA6U0N6pOY6VKxELwyPuwdklDdwNoJwiLnngorP0mYg.PNG.tdj04131/Menu.png?type=w773"></td>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMjZfMTMz/MDAxNjcyMDE5OTg5MDQ4.v1P4pMuUd0lDA6Bc1RlgES1wg-K-XBLjKUnKL5jC7Ukg.-2Tp-q5e-eYwj7zuRMUI2SGI9-9Ab40mBSA8qNfQwSsg.PNG.tdj04131/Pick.png?type=w773"></td>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMjZfMTUx/MDAxNjcyMDE5OTg4OTQ3.6_83y5y_XYQBD1tYzMN8VZSBJlddaX4_tTnu-Hg6Pr0g.t3ut_Hu1WJR_wOYiRIOZNtzxdCzru3YsbpIJIC-pyjgg.PNG.tdj04131/SetUp.png?type=w773"></td>
      </table>
  </div>
    <div>
    <h2> 사용 영상 </h2>
    https://youtu.be/VFF3JZuiE6o
  </div>
  <div>
    <h2> 배운 점 </h2>
      유니티 내장 함수를 이용해 다른 폴더 안의 Sprite를 불러오고 출력해보았다.<br><br>
      
  </div>
  <div>
    <h2> 수정할 점 </h2>
      폭탄 터지는 시간과 게임이 끝나는 시간 간의 오차.<br><br>
      추가적인 컨텐츠
   <h2> Design Picture </h2>
   <table>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMDJfMTcz/MDAxNjY5OTQ1MTYyNTcz.xRGtDzHsxcJYazZlDcthq5OryoHRCOAIo3IhGdm3-4sg.GZeppaeShzgz5M3EIWUjWJXTdv0lI3WDgx6GlKBlis8g.JPEG.tdj04131/KakaoTalk_20221202_103401185.jpg?type=w773" height = 500></td>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjEyMDJfNDYg/MDAxNjY5OTQ1MTYyNTU0.1gzPKWdthy-1HV3kGPMn-xFlpEmNQUljsOlQcorqdpwg.WTibCAZObK__76rH2hzr5SLjkZHd9qkVYY0WdQ_0MQ4g.JPEG.tdj04131/KakaoTalk_20221202_103401185_01.jpg?type=w773" height = 500></td>
      </table>
  </div>
   <div>
       <h2> 주요 코드 </h2>
       <h4> MainSM SetUp 함수 </h4>
    </div>
    
```csharp
situationNum = Random.Range(0, 4);

mainSprite = Resources.Load<Sprite>("Arts/MainImage/" + situationNum);
midSprite = Resources.Load<Sprite>("Arts/MiddleImage/" + situationNum);

screenImage.GetComponent<SpriteRenderer>().sprite = mainSprite;
```
