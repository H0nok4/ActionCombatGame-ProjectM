using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlotSystem {
    public enum ScenarioErro {
        NullObject,
    }

    public enum ScenarioCommandType {
        startCommand, textCommand, ifgotoCommand, gotoCommand, optionCommand, flagCommand, endCommand
    }

    public class ScenarioCommand {
        public ScenarioCommandType type;
        public string[] parameter;
        public virtual void Run() {

        }
    }

    public class TextCommand : ScenarioCommand {
        public TextCommand(string[] parameter) {
            type = ScenarioCommandType.textCommand;
            this.parameter = parameter;
            //text��parameter�У�����Ϊ0������ʾ�ı���Position��1Ϊ��ʾ�ı������֣�������ǲ�ͬ�����
        }

        public override void Run() {
            //TO DO:�����ı�
            //Debug.Log("Show Text");
            GameDirecter.instance.state = ScenarioPlayStatus.waitTextWriteDone;
            UIManager.instance.WritingText(parameter[0],parameter[1],parameter[2],true);
        }
    }

    public class IfGotoCommand : ScenarioCommand {
        public IfGotoCommand(string[] parameter) {
            type = ScenarioCommandType.ifgotoCommand;
            this.parameter = parameter;
            //IfGoto��parameter�У�����Ϊ0��������,һ��Ϊһ���������������˾籾������Ķ�ջ�1Ϊȥ��λ�õı�����Ҳ�����ھ籾���ݵĶ�ջ��
        }

        public override void Run() {
            var conditionValue = GameDirecter.instance.GetValueFromHeap(parameter[0]);
            if (conditionValue != -1) {
                switch (parameter[1]) {
                    case "==":
                    if (conditionValue == int.Parse(parameter[2])) {
                        GameDirecter.instance.curCommandPosition = GameDirecter.instance.GetValueFromHeap(parameter[3]);
                        GameDirecter.instance.NextCommand();
                        Debug.Log($"True!Jump to {parameter[3]}");
                        return;
                    }
                    break;

                }
            }
            GameDirecter.instance.NextCommand();
        }
    }

    public class GotoCommand : ScenarioCommand {
        public GotoCommand(string[] parameter) {
            type = ScenarioCommandType.gotoCommand;
            this.parameter = parameter;
            //goto��parameter�У�����Ϊ0����ȥ��λ�õı����������ھ籾���ݵĶ�ջ��
        }

        public override void Run() {
            GameDirecter.instance.curCommandPosition = GameDirecter.instance.GetValueFromHeap(parameter[0]);
            GameDirecter.instance.NextCommand();
        }
    }

    public class FlagCommand : ScenarioCommand {
        public FlagCommand(string[] parameter) {
            type = ScenarioCommandType.flagCommand;
            this.parameter = parameter;
            //flag��parameter�У�����Ϊ0��������Ҫ�����ھ籾���ݵĶ�ջ��ı��������֣�1��λ��
        }

        public override void Run() {
            GameDirecter.instance.AddVariable(parameter[0],int.Parse(parameter[1]));
        }
    }

    public class OptionCommand : ScenarioCommand {
        public OptionCommand(string[] parameter) {
            type = ScenarioCommandType.optionCommand;
            this.parameter = parameter;
            //option��parameter�У�����Ϊż��Ϊѡ�����������Ϊѡ���Text
        }

        public override void Run() {
            GameDirecter.instance.state = ScenarioPlayStatus.waitPlayerSelectOption;
            if (parameter.Length % 2 != 0) {
                Debug.LogError($"Error:Option Need a legalVar And legal text");
            }
            for (int i = 0;i < parameter.Length - 1;i += 2) {
                UIManager.instance.SetOptionBox(parameter[i],parameter[i + 1]);
                GameDirecter.instance.AddVariable(parameter[i],0);
                /*�����ı������ӵ���ջ�У�ż����Text����ӵ�UIManager�ĶԻ�����
                 * ����һ����ѡ�����ʾText���֣�����Ҳ���ϱ�����
                 * �ڵ����ʱ��ı��������ֵ����������ifgoto�ж�*/
            }
        }

    }

    public class StartCommand : ScenarioCommand {
        public StartCommand(string[] parameter) {
            type = ScenarioCommandType.startCommand;
            this.parameter = parameter;
            //start��parameter�У�������0��ʼ��Start���ڵ�ָ��λ��
        }

        public override void Run() {
            Debug.Log("Start");
        }
    }

    public class EndCommand : ScenarioCommand {
        public EndCommand(string[] parameter) {
            type = ScenarioCommandType.endCommand;
            this.parameter = parameter;
            //end��parameter�У�������0��ʼ��End���ڵ�ָ��λ��
        }

        public override void Run() {
            GameDirecter.instance.EndPlayScenario();
        }
    }
}
