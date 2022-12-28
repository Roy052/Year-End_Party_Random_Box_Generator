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
     2022년 온라인으로 진행된 송년회가 <br><br>
    <h2> 사용 방식 </h2>
      <h3> 실행 </h3>
      zip 파일을 압축 푼 뒤, Year-End_Party_Random_Box_Generator.exe 파일을 실행합니다.
      <h3> 선물 추가 </h3>
      메인 메뉴에서 설정 버튼(모양 + 텍스트)을 클릭합니다.<br><br>
      Assets 폴더 안에 GiftInfo 폴더(/Assets/GiftInfo) 안에 160px X 160px 크기의 이미지를 추가합니다. <br><br>
      게임을 시작한 후에 사진을 추가한 경우 이미지 리로드 버튼을 눌러야 합니다.<br><br>
      중간에 있는 드롭박스에서 사진을 선택하고, 선물 이름을 입력하고, 선물 수량을 입력한 뒤 선물 넣기 버튼을 누릅니다.<br><br>
      <h3> 선물 뽑기 </h3>
      메인 메뉴에서 뽑기 버튼(모양 + 텍스트)을 클릭합니다.<br><br>
      선물이 이미 등록되어있다면 카드 뽑기 단계로 넘어가고 아니면 선물을 추가하라는 메시지가 나옵니다.<br><br>
      카드 뽑기 단계에선 3장의 카드가 등장하고 그 중 1장을 뽑으면 선물이 나타납니다.<br><br>
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
      C# File IO를 활용해 사진 데이터를 불러와 Unity 내장함수로 Sprite로 바꾸어 보았다.<br><br>
      또한 선물 데이터를 json 파일로 저장해 계속 불러올 수 있게 할 수 있었다.<br><br>
      같이 송년회를 했던 친구들의 피드백으로 사진이 밀리는 버그부터 클릭 문제, 세이브 문제 등등 많이 수정할 수 있었다.<br><br>
  </div>
  <div>
    <h2> 수정할 점 </h2>
      카드 이미지 바꾸기<br><br>
      SFX 추가<br><br>
   <div>
       <h2> 주요 코드 </h2>
       <h4> RandomBox LoadImage 함수 </h4>
    </div>
    
```csharp
public void LoadImage()
{
    sprites = new List<Sprite>();
    int count = 0;
    try
    {
        while (count < 100)
        {
            string path = "./Assets/GiftInfo/" + count + ".png";
            //Byte로 읽은 걸 Texture로 변환 후 sprite로 변환
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(64, 64);
                texture.LoadImage(data);
                texture.name = count.ToString();
                Sprite sprite = Sprite.Create(texture, 
                    new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprites.Add(sprite);
            }
            else
            {
                break;
            }
            count++;
        }
    }
    catch (FileNotFoundException e)
    {
        Debug.Log("The file was not found:" + e.Message);
    }
    catch (DirectoryNotFoundException e)
    {
        Debug.Log("The directory was not found: " + e.Message);
    }
    catch (IOException e)
    {
        Debug.Log("The file could not be opened:" + e.Message);
    }
}
```
