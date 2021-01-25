import ApiRequest from "../../Api/apiRequest";
import { IAlertModal } from "../../Shared/Modals/alertDialog";
import { IAddSubscriberDto, IUpdateSubscriberDto } from "../../Api/Models";
import { API_COMMAND_ADD_SUBSCRIBER, API_COMMAND_UPDATE_SUBSCRIBER } from "../../Shared/constants";
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

export async function UpdateSubscriberData(PayLoad: IUpdateSubscriberDto): Promise<IAlertModal>
{

    const apiRequest = ApiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_UPDATE_SUBSCRIBER, 
        data: 
        { 
            id: PayLoad.id,
            email: PayLoad.email,
            isActivated: PayLoad.isActivated,
            count: PayLoad.count
        } 
    });

    const results = await apiRequest;

    if (results.isSucceeded)
    {
        return { 
            State: true, 
            Titile: "Subscriber email update", 
            Message: GetNewsletterSuccess(), 
            Icon: 0 
        };
    }
    else
    {
        return { 
            State: true, 
            Titile: "Subscriber email update | Error", 
            Message: GetNewsletterError(results.error.ErrorMessage), 
            Icon: 2 
        };
    }

}
