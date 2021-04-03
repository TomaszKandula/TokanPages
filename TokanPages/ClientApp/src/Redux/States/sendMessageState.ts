import { SendMessageEnum } from "../../Redux/Enums/sendMessageEnum";

export interface ISendMessage
{
    isSendingMessage: SendMessageEnum;
    hasSentMessage: boolean;
}
