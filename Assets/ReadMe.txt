######2D횡스크롤 스타일 미니게임 <Green Slime>-Beta0.101######
- 이 파일은 게임을 실행하고 뜯으며 봐주세요.

###인기게임인 테라리아(Terraria)와 파인딩블루(Finding Blue)에서 
영감을 받아 만든 게임입니다.
제작까지 하루에 평균 3시간정도를 투자해 
한달하고 4일 남짓 걸렸습니다.
원래 구현하고싶은것들이 있었는데,
시간이 부족한탓에 구현하지 못했습니다.
구현하고 싶었던것들은 맨 밑에 후술합니다.
---
###워크플로우에 관한 설명입니다.
엔진은 Unity, 언어는 C#을 채용했고, 에셋스토어는 일절 사용하지 않았습니다.
처음부터 끝까지 혼자 만들었으며, 모르는것은 UnityMenual문서와
한국 유니티사용자 카페인 UnityHub을 참고했습니다.
이와 관련한 내용은 <2D횡스크롤게임 만드면서 다른사람스크립트 인용한것>
txt를 참고해 주세요.
또, 제작과 관련한 정보는 게임 메인화면의 Credit을 봐주세요.
스크립트는 약 42개가 있으며 변수, 함수 등의 네이밍은 구글번역기를 사용했고,
알고리즘 관련한 부분은 주석을 사용했습니다.
자주 사용하는 함수에는 C# XML주석을 사용했습니다.
---
###게임 내 전체 진행에 관한 설명입니다.
게임을 시작하면, 메인씬에 있는 싱글톤 패턴을 적용한 게임 매니저가 
에셋들을 보관한 매니저 프리팹들을 인스턴스화합니다.
(이 매니저 프리팹들은 Resource함수는 비용이 비싸므로
각자 자기 카테고리에 맞는 에셋들을 보관하고 있습니다)
게임매니저는 사용자 데이터 관련해 세이브가 있다면 불러옵니다.
또, 모든 아이템들의 프리팹과 키를 ItemBox 스크립트를 통해
가지고 있습니다.
(이 ItemBox스크립트는 플레이어가 상점의 아이템을 구매하고,
실제 스테이지에서 사용하거나 세이브할때 사용됩니다.)
MenuPanel에서 버튼을 클릭해 선택한 스테이지로 이동합니다.
버튼엔 SceneChanger스크립트가 붙어있어 인스펙터에 할당한 Scene이름으로
현재 Scene을 바꾸고, 할당한 BGM으로 인스턴스화한 사운드매니저의
SoundSource를 바꿔 재생할 BGM을 바꿉니다.
스테이지에서 나갈땐 Esc키를 눌러 인스턴스화한 GUI매니저의 패널을 활성화해
MainMenu버튼을 누르거나, 스테이지 내의 SlimeCoin을 찾아서 나갈 수 있습니다.
SlimeCoin은 스테이지당 하나를 얻을 수 있으며, 상점에서 사용할 수 있습니다.
상점에서 아이템을 사면 게임매니저의 List<int>형 itemList에 아이템의 키값을 추가하고,
스테이지로 이동할 때 Scene에 이미 할당되어있는 게임매니저가 싱글톤패턴으로 
삭제되기 전에 itemList의 키값과 itemBox스크립트의 Dictionary<int, GameObjct>를 이용해
아이템 프리팹들을 생성합니다.
끝으로, 게임을 나갈때엔 메인화면에서 Save&Program Exit버튼을 클릭해
게임매니저의 SaveBinary()함수를 실행하고 종료합니다.
이 SaveBinary()함수는 Dictionary로 보관하고있던 현재 스테이지를 얼마나 진행했는지에 대한
정보와 GoldCoin, SlimeCoin, 구매한 아이템 정보 등을 저장해주는 책임을 갖습니다.
보통 C:/Users/user/AppData/LocalLow/DefaultCompany/2D GameMaster/ 에 저장됩니다.

---
####구현하고 싶었던 것들

총 13*5개의 일반, 전문가, 이벤트난이도의 스테이지들
-현재 튜토리얼만 구현했습니다.
-스테이지마다 각기 다른 BGM을 넣으려고 유튜브에서 사용 허가까지 받아뒀으나,
사용하지 못했습니다.

총 50개의 무기와 시스템 아이템들
-현재 4개가 구현되어 있습니다.

상점
-아이템에 대한 갓챠시스템 도입

설정들
-아직 Effect설정과 Language설정이 구현되지 않았습니다.

Housing버튼
-슬라임키우기, 게임시작화면 꾸미기 등을 계획했으나 구현되지 않았습니다.

플랫폼을 모바일로 전환
-초기 기획당시 파인딩블루에서 영감을 받은 게임이라
모바일로 만들고 싶었습니다.

