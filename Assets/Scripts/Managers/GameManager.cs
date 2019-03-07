using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class GameManager : MonoBehaviour
{
    //각 매니저들과 게임 내 오브젝트들을 연결시켜주는 책임을 가진다.
    //각 매니저들의 싱글턴과 공통 함수들을 부모클래스의 함수를 통해 실행하는 책임을 가진다.
    //스테이지와 씬을 관리하는 책임을 가진다.
    public ItemBox itemBox;
    public List<string> managerKeys;
    public List<SuperManager> managerValues;
    public Dictionary<string, SuperManager> managers = new Dictionary<string, SuperManager>();
    //딕셔너리는 인스펙터에 나오지 않으므로 List로 Key와 Value를 나눴다.

    public int goldCoin = 0;
    public int slimeCoin = 0;
    public Dictionary<int, Dictionary<int, bool>> canAddSlimeCoin=new Dictionary<int, Dictionary<int, bool>>();//기본값은 모든스테이지에서 한번씩 얻을 수 있으므로 true이다.
    public Dictionary<int, Dictionary<int, bool>> isStageLocked=new Dictionary<int, Dictionary<int, bool>>();//기본값은 스테이지가 잠긴상태이므로 true이다.
    public List<int> buyItemKeys = new List<int>(); //상점에서 구매한 아이템 프리팹들이다.

    public int[] stageBox;//딕셔너리를 초기화할때 스테이지마다 내부스테이지 갯수가 다를 수 있으므로 이를 위한 내부스테이지 갯수들의 배열이다.
    public string nowPlayingScnenName = "MainMenu";//Scene 재시작을 위해 사용된다.
    [System.Serializable]
    [XmlRoot("GameData")]
    public class MySaveData
    {
        [System.Serializable]
        public struct GameData
        {
            public int goldCoin;
            public int slimeCoin;
            public Dictionary<int, Dictionary<int, bool>> isStageLocked;
            public Dictionary<int, Dictionary<int, bool>> canAddSlimeCoin;
            public List<int> buyItemKeys;
        }

        public GameData MyGameData = new GameData();
    }

    public MySaveData myData = new MySaveData();

    public Vector3 buyingItemSpown=new Vector3(0,0,0);
    public Vector3 outerWorld;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;
    
    void Awake()
    {
        if (instance)
        {
            MakeToItems();
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        //게임매니저의 싱글턴 패턴을 적용했다.

        int i = 0;
        foreach (SuperManager o in managerValues)
        {     
            managers.Add(managerKeys[i],Instantiate(o, Vector2.zero, Quaternion.identity));
            //매니저들을 인스턴스화한다.
            i++;
        }

        LoadBinary(Application.persistentDataPath + "/Mydata.sav");
        Debug.Log(Application.persistentDataPath + "/Mydata.sav");
    }

    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/Mydata.sav"))
        {
            for (int outsideIndex = 1; outsideIndex < stageBox.Length + 1; outsideIndex++)
            {
                Dictionary<int, bool> insideStageLockedInfo = new Dictionary<int, bool>();
                for (int insideIndex = 1; insideIndex < stageBox[outsideIndex - 1] + 1; insideIndex++)
                {//스테이지마다 내부스테이지 갯수가 다를 수 있으므로 for문을 두번돌리고 배열을 사용했다.
                    if (outsideIndex == 1 && insideIndex == 1)
                    {
                        insideStageLockedInfo.Add(insideIndex, false);//첫번째 스테이지는 무조건 열려있어야한다.
                        continue;//첫번째 스테이지에 대한 설정은 이미 완료했다.
                    }
                    insideStageLockedInfo.Add(insideIndex, true);


                }
                isStageLocked.Add(outsideIndex, insideStageLockedInfo); ;//스테이지 잠금에 대한 딕셔너리값 추가
            }

            for (int outsideIndex = 1; outsideIndex < stageBox.Length + 1; outsideIndex++)
            {
                Dictionary<int, bool> insideStageCanGetSlimeCoinInfo = new Dictionary<int, bool>();
                for (int insideIndex = 1; insideIndex < stageBox[outsideIndex - 1] + 1; insideIndex++)
                {//스테이지마다 내부스테이지 갯수가 다를 수 있으므로 for문을 두번돌리고 배열을 사용했다.
                    insideStageCanGetSlimeCoinInfo.Add(insideIndex, true);
                }
                canAddSlimeCoin.Add(outsideIndex, insideStageCanGetSlimeCoinInfo);//스테이지에서 Slime코인을 얻을 수 있는지에 대한 딕셔너리값 추가
            }
        }
    }

    /// <summary>
    /// 게임데이터를 세이브할때 사용되는 함수이다.
    /// </summary>
    private void GetGameData()
    {
      myData.MyGameData.isStageLocked = isStageLocked;
       myData.MyGameData.canAddSlimeCoin = canAddSlimeCoin;
        myData.MyGameData.buyItemKeys = buyItemKeys;
        myData.MyGameData.goldCoin = goldCoin;
        myData.MyGameData.slimeCoin = slimeCoin;
        
    }
    
    /// <summary>
    /// 게임데이터를 불러올때 사용되는 함수이다.
    /// </summary>
    private void SetGameData()
    {
     isStageLocked = myData.MyGameData.isStageLocked;
      canAddSlimeCoin = myData.MyGameData.canAddSlimeCoin;
       buyItemKeys = myData.MyGameData.buyItemKeys;
        goldCoin = myData.MyGameData.goldCoin;
        slimeCoin = myData.MyGameData.slimeCoin;
    }

    /// <summary>
    /// SaveBinary(Application.persistentDataPath + "/Mydata.sav");같은 식으로 호출한다.
    /// </summary>
    /// <param name="fileName"></param>
    public void SaveBinary(string fileName = "GetGameData.sav")
    {
        GetGameData();

        //게임데이터를 저장한다.
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = File.Create(fileName);
        bf.Serialize(stream, myData);
        stream.Close();
    }

    /// <summary>
    /// LoadBinary(Application.persistentDataPath + "/Mydata.sav");같은 식으로 호출한다.
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadBinary(string fileName = "GetGameData.sav")
    {
        if (!File.Exists(fileName)) return;


        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = File.Open(fileName, FileMode.Open);
        myData = bf.Deserialize(stream) as MySaveData;
        stream.Close();

        SetGameData();
    }

    /// <summary>
    /// Scene이 바뀔때 해당 씬의 게임매니저가 이 함수를 호출하며 자신을 삭제한다.
    /// </summary>
    public void MakeToItems()
    {
        Debug.Log("Make!");
        foreach (int code in GameManager.Instance.buyItemKeys)
        {
            Instantiate(GameManager.Instance.itemBox.itemList[code], buyingItemSpown, Quaternion.identity);
        }
    }

    /// <summary>
    /// SlimeCoin 1개를 추가하고 스테이지에서 얻을수있는지를 비활성화한다.
    /// </summary>
    /// <param name="stage"></param>
    /// <param name="insideStage"></param>
    public void AddSlimeCoin(int stageNum, int insideStageNum)
    {
        if (canAddSlimeCoin[stageNum][insideStageNum] != false)
        {
            slimeCoin++;
            canAddSlimeCoin[stageNum][insideStageNum] = false;
        }
    }

    /// <summary>
    /// GoldCoin을 지정한 값과 더한다.
    /// </summary>
    /// <param name="coin"></param>
    public void AddGoldCoin(int coin)
    {
        goldCoin += coin;
    }

    /// <summary>
    /// 지정한 해당 스테이지의 내부스테이지의 잠금을 해제한다.
    /// </summary>
    /// <param name="stage"></param>
    /// <param name="insideStage"></param>
    public void StageLockRelease(int stageNum, int insideStageNum)
    {
        isStageLocked[stageNum][insideStageNum] = false;
    }

}
