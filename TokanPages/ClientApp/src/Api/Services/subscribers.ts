import ApiRequest from "../../Api/apiRequest";
import { IAlertModal } from "../../Shared/Modals/alertDialog";
import { IAddSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER } from "../../Shared/constants";
import { GetNewsletterError, GetNewsletterSuccess } from "../../Shared/Modals/messageHelper";

export async function AddSubscriber(PayLoad: IAddSubscriberDto): Promise<IAlertModal>
{

    const apiRequest = ApiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_ADD_SUBSCRIBER, 
        data: 
        { 
            email: PayLoad.email 
        } 
    });

    const results = await apiRequest;

    if (results.isSucceeded)
    {
        return { 
            State: true, 
            Titile: "Newsletter", 
            Message: GetNewsletterSuccess(), 
            Icon: 0 
        };
    }
    else
    {
        return { 
            State: true, 
            Titile: "Newsletter | Error", 
            Message: GetNewsletterError(results.error.ErrorMessage), 
            Icon: 2 
        };
    }

}
