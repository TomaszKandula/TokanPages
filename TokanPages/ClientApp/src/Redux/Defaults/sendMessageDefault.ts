import { SendMessageEnum } from "../../Redux/Enums/sendMessageEnum";
import { ISendMessage } from "../../Redux/States/sendMessageState";

export const SendMessageDefaultValues: ISendMessage = 
{
    isSendingMessage: SendMessageEnum.notStarted,
    hasSentMessage: false
}
