using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlotSystem {
    public enum ScenarioPlayStatus {
        start, waitTextWriteDone, waitPlayerSelectOption, wait, end, waitForNewScenario
    }

    public class GameDirecter : MonoBehaviour {
        public TextScript textScript = new TextScript();
        [SerializeField] Dictionary<string,int> ScenarioHeap = new Dictionary<string,int>();

        public ScenarioPlayStatus state;
        public string StartScenario;
        public string EndScenario;

        public List<ScenarioCommand> curScenarioCommands;
        public int curCommandPosition = 0;

        public static GameDirecter instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                if (instance != this) {
                    Destroy(gameObject);
                }
            }

        }

        public void HandleUpdate() {
            if (state == ScenarioPlayStatus.start) {
                curCommandPosition++;
            } else if (state == ScenarioPlayStatus.waitTextWriteDone) {
                HandleTextWriteDone();
            } else if (state == ScenarioPlayStatus.wait) {
                HandlePlayNextCommand();
            }
        }

        public void HandleTextWriteDone() {
            if (PlayerController.Instance.GetPressAttackButton()) {
                if (UIManager.instance.isWriting) {
                    UIManager.instance.WriteTextImmediately();
                }
            }
        }

        public void HandlePlayNextCommand() {
            if (PlayerController.Instance.GetPressAttackButton()) {
                curScenarioCommands[++curCommandPosition].Run();
            }
        }

        public void HandlePlayerSelectOption(string OptionVar) {
            UIManager.instance.ClearOptions();
            ChangeVariable(OptionVar,1);
            NextCommand();
            Debug.Log($"CurCommand.Type = {curScenarioCommands[curCommandPosition].type}");
        }

        #region ���ž籾
        public void PlayScenario(string Scenario) {
            GameManager.Instance.GameState = GameState.PlayScenario;
            StartCoroutine(StartPlayScenario(Scenario));
        }

        IEnumerator StartPlayScenario(string Scenario) {
            //TODO:�л�����һ����Ϸ״̬ GameManager.Instance.SwitchGameState(GameState.PlayScenario);
            state = ScenarioPlayStatus.start;//TODO:test
            var texts = textScript.LoadScenarioTexts(Scenario);
            curScenarioCommands = textScript.CreatCommands(texts);
            curCommandPosition = 0;
            foreach (var command in curScenarioCommands) {
                if (command.type == ScenarioCommandType.flagCommand) {
                    command.Run();
                }
            }

            curScenarioCommands[++curCommandPosition].Run();
            yield return null;
        }
        #endregion

        public void NextCommand() {
            state = ScenarioPlayStatus.wait;
            curScenarioCommands[++curCommandPosition].Run();
        }

        public void EndPlayScenario() {
            //TODO:��ת��Ϸ״̬ GameManager.instance.ReverseGameState();
            GameManager.Instance.GameState = GameState.Battle;//TODO:test
            state = ScenarioPlayStatus.waitForNewScenario;
            curScenarioCommands.Clear();
            curCommandPosition = 0;
            ScenarioHeap.Clear();
            UIManager.instance.ClearAll();
        }

        #region ������ջ
        public void AddVariable(string variableName,int value) {
            if (!ScenarioHeap.ContainsKey(variableName)) {
                ScenarioHeap.Add(variableName,value);
            } else {
                Debug.LogError($"Error:SameKeyInScenarioHeap KeyName = {variableName}");
            }

        }

        public void ChangeVariable(string variableName,int value) {
            if (ScenarioHeap.ContainsKey(variableName)) {
                ScenarioHeap[variableName] = value;
            } else {
                Debug.LogError($"Error:NoneKeyInScenarioHeap KeyName = {variableName}");
            }
        }

        public int GetValueFromHeap(string variable) {
            if (ScenarioHeap.ContainsKey(variable)) {
                return ScenarioHeap[variable];
            }

            Debug.LogError($"Error:NoneKeyInScenarioHeap KeyName = {variable}");
            return -1;
        }
        #endregion
    }
}
