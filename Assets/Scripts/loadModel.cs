using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class loadModel : MonoBehaviour
{
    public class mageContract
    {
        public static string abi = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256 []\",\"name\":\"ids\",\"type\":\"uint256 []\"},{\"indexed\":false,\"internalType\":\"uint256 []\",\"name\":\"values\",\"type\":\"uint256 []\"}],\"name\":\"TransferBatch\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"TransferSingle\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"value\",\"type\":\"string\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"URI\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address []\",\"name\":\"accounts\",\"type\":\"address []\"},{\"internalType\":\"uint256 []\",\"name\":\"ids\",\"type\":\"uint256 []\"}],\"name\":\"balanceOfBatch\",\"outputs\":[{\"internalType\":\"uint256 []\",\"name\":\"\",\"type\":\"uint256 []\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"mintRandomMage\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeBatchTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"uri\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        public static string address = "0x9140e0F102D290a70Db61f5b6649c8f1090f5B8A";


    }
    // Start is called before the first frame update
    public GameObject head, mageHat; 
    public GameObject body { get; set; }
    public GameObject armLeft { get; set; }
    public GameObject armRight { get; set; }

    public List<string> balanceAddress = new List<string>();

    public List<BigInteger> balanceIds = new List<BigInteger>();

    public static void UpdateGuiText(string name, string text)
    {
        var outputText = GameObject.Find(name);
        if (outputText.GetComponent<Text>() == null)
        {
            outputText.GetComponent<InputField>().text = text;

        }
        else
        {
            outputText.GetComponent<Text>().text = text;
        }
    }

    void Start()
    {
       

        GameObject.Find("newWallet").
          GetComponent<Button>().
          onClick.AddListener(newWalletHandler);
        GameObject.Find("importWallet").
         GetComponent<Button>().
         onClick.AddListener(importWalletHandler);
        GameObject.Find("mintMage").
         GetComponent<Button>().
         onClick.AddListener(mintMageHandler);
        GameObject.Find("importMage").
         GetComponent<Button>().
         onClick.AddListener(importMageHandler);
        GameObject.Find("ExitBtn").
         GetComponent<Button>().
         onClick.AddListener(quit);

        for (int i = 0; i <= 99; i++)
        {
            var id = new BigInteger(i);
            balanceIds.Add(id);
        }

    }

    private void quit()
    {
        Application.Quit();
    }

    private void newWalletHandler()
    {
        var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
        var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
        var account = new Account(privateKey);
        UpdateGuiText("privateKey", account.PrivateKey);
        UpdateGuiText("address", account.Address);
        Debug.Log("new");
    }

    [Function("balanceOf", "uint256")]
    public class balanceOfFunction : FunctionMessage
    {
        [Parameter("address", "accounts", 1)]
        public string address { get; set; }
        [Parameter("uint256", "id", 2)]
        public BigInteger id { get; set; }
    }

    [Function("balanceOfBatch", "uint256[]")]
    public class balanceOfBatchFunction : FunctionMessage
    {
        [Parameter("address[]", "accounts", 1)]
        public List<string> address { get; set; }
        [Parameter("uint256[]", "ids", 2)]
        public List<BigInteger> ids { get; set; }
    }
    private async void balanceOfMages()
    {
        var address = GameObject.Find("address").GetComponent<InputField>().text;
        var imporMageInput = GameObject.Find("imporMageInput").GetComponent<InputField>().text;
        if (address.Length > 5 && imporMageInput.Length > 0)
        {
          

            var web3 = new Web3("https://rpc-mumbai.maticvigil.com/");
            

            var balanceOfFunctionMessage = new balanceOfFunction()
            {
                address = address,
                id = new BigInteger(int.Parse(imporMageInput))
            };

            var balanceHandler = web3.Eth.GetContractQueryHandler<balanceOfFunction>();
            var balance = await balanceHandler.QueryAsync<BigInteger>(mageContract.address, balanceOfFunctionMessage);

            Debug.Log(balance);
            if (balance != 0)
            {
                
               
                var gltf = gameObject.AddComponent<GLTFast.GltfAsset>();
                gltf.url = $"https://storageapi.fleek.co/lucasespinosa28-team-bucket/mageNft/mage{imporMageInput}.gltf";

                head = GameObject.Find("Head");
                body = GameObject.Find("Body");
                armLeft = GameObject.Find("armLeft");
                armRight = GameObject.Find("armRight");

                

            }
           
        }
    }
    private async void balanceOfBatchMages()
    {
        var address = GameObject.Find("address").GetComponent<InputField>().text;
        if (address.Length > 5)
        {
            if (balanceAddress.Count == 0)
            {
                for (int i = 0; i <= 99; i++)
                {
                    balanceAddress.Add(address);
                }
            }

            var web3 = new Web3("https://rpc-mumbai.maticvigil.com/");
            //var balance = await web3.Eth.GetBalance.SendRequestAsync("0x01eAbdB2a52f805F1d56809C6fc51D8aED598f71");


            var balanceOfFunctionMessage = new balanceOfBatchFunction()
            {
                address = balanceAddress,
                ids = balanceIds
            };

            var balanceHandler = web3.Eth.GetContractQueryHandler<balanceOfBatchFunction>();
            var balance = await balanceHandler.QueryAsync<List<BigInteger>>(mageContract.address, balanceOfFunctionMessage);

            var balanceTexts = "Balance of mages:";
            for (int i = 0; i < balance.Count; i++)
            {
                if (balance[i] != 0)
                {
                    balanceTexts += $"#{i} ";
                }

            }
            UpdateGuiText("balance", balanceTexts);
        }
    }
    private void importWalletHandler()
    {
        var privateKey = GameObject.Find("privateKey").GetComponent<InputField>().text;
        if (privateKey.Length > 60)
        {
            var account = new Account(privateKey);
            UpdateGuiText("address", account.Address);
            UpdateGuiText("privateKey", account.PrivateKey);

            balanceOfBatchMages();
        }
    }

    [Function("mintRandomMage")]
    public class mintHandler : FunctionMessage
    {

        [Parameter("uint256", "id", 1)]
        public BigInteger id { get; set; }
    }

    
    private async void mintMageHandler()
    {
        var mintId = GameObject.Find("mageId").GetComponent<InputField>().text;
        if (mintId.Length > 0 && mintId.Length < 3)
        {
            var privateKey = GameObject.Find("privateKey").GetComponent<InputField>().text;
            if (privateKey.Length > 5)
            {
                var account = new Account(privateKey);
                var web3 = new Web3(account, "https://rpc-mumbai.maticvigil.com/");
                //var balance = await web3.Eth.GetBalance.SendRequestAsync("0x01eAbdB2a52f805F1d56809C6fc51D8aED598f71");

                var transferHandler = web3.Eth.GetContractTransactionHandler<mintHandler>();
                var transfer = new mintHandler()
                {
                    id = new BigInteger(int.Parse(mintId))
                };
                var transactionReceipt = await transferHandler.SendRequestAndWaitForReceiptAsync(mageContract.address, transfer);
                balanceOfBatchMages();

            }
        }
    }
    private void importMageHandler()
    {
        var scene = GameObject.Find("Scene");
        var player = GameObject.Find("Player(Clone)");
        if (player != null && scene != null)
        {
            Destroy(scene);
            Destroy(GameObject.Find("Player(Clone)"));
        }
        Instantiate(Resources.Load<GameObject>("Player"));
        balanceOfMages();
    }
    // Update is called once per frame
    void Update()
    {
       
        var mageHat = GameObject.Find("character_mageHat");
        if (mageHat != null)
        {
            mageHat.transform.parent = head.transform;
        }

        var mageHair = GameObject.Find("character_mageHair");
        if (mageHair != null)
        {
            mageHair.transform.parent = head.transform;
        }

        var mageHead = GameObject.Find("character_mageHair");
        if (mageHead != null)
        {
            mageHair.transform.parent = head.transform;
        }

        var mageBody = GameObject.Find("character_mageBody");
        if (mageHead != null)
        {
            mageBody.transform.parent = body.transform;
        }
        var mageArmRight = GameObject.Find("character_mageArmRight");
        if (mageArmRight != null)
        {
            mageArmRight.transform.parent = armRight.transform;
        }

        var mageArnLeft = GameObject.Find("character_mageArnLeft");
        if (mageArnLeft != null)
        {
            mageArnLeft.transform.parent = armLeft.transform;
        }

    }

   
}
