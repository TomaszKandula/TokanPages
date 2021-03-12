import { apiRequest } from "../requests";
import { IAlertModal } from "../../Shared/Modals/alertDialog";
import { ISendMessageDto } from "../../Api/Models";
import { API_COMMAND_SEND_MESSAGE } from "../../Shared/constants";
import { GetMessageOutError, GetMessageOutSuccess } from "../../Shared/Modals/messageHelper";

export const SendMessage = async (PayLoad: ISendMessageDto): Promise<IAlertModal> =>
{
    const request = apiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_SEND_MESSAGE, 
        data: 
        { 
            firstName: PayLoad.firstName,
            lastName:  PayLoad.lastName,
            userEmail: PayLoad.userEmail,
            emailFrom: PayLoad.emailFrom,
            emailTos:  PayLoad.emailTos,
            subject:   PayLoad.subject,
            message:   PayLoad.message
        }
    });

    const results = await request;
    return results.isSucceeded ? { 
        State: true, 
        Titile: "Contact Form", 
        Message: GetMessageOutSuccess(), 
        Icon: 0 
    } : { 
        State: true, 
        Titile: "Contact Form | Error", 
        Message: GetMessageOutError(results.error.ErrorMessage), 
        Icon: 2 
    };
}
