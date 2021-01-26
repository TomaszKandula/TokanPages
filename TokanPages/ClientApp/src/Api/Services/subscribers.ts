import ApiRequest from "../../Api/apiRequest";
import { IAlertModal } from "../../Shared/Modals/alertDialog";
import { IAddSubscriberDto, IUpdateSubscriberDto, IRemoveSubscriberDto } from "../../Api/Models";
import { GetNewsletterError, GetNewsletterSuccess } from "../../Shared/Modals/messageHelper";
import { 
    API_COMMAND_ADD_SUBSCRIBER, 
    API_COMMAND_REMOVE_SUBSCRIBER, 
    API_COMMAND_UPDATE_SUBSCRIBER 
} from "../../Shared/constants";

export const AddNewSubscriber = async (PayLoad: IAddSubscriberDto): Promise<IAlertModal> =>
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

export const UpdateSubscriberData = async (PayLoad: IUpdateSubscriberDto): Promise<IAlertModal> =>
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

export const RemoveSubscriberData = async (PayLoad: IRemoveSubscriberDto): Promise<IAlertModal> =>
{
    const apiRequest = ApiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_REMOVE_SUBSCRIBER, 
        data: 
        { 
            id: PayLoad.id
        } 
    });

    const results = await apiRequest;

    if (results.isSucceeded)
    {
        return { 
            State: true, 
            Titile: "Unsubscribe", 
            Message: GetNewsletterSuccess(), 
            Icon: 0 
        };
    }
    else
    {
        return { 
            State: true, 
            Titile: "Unsubscribe | Error", 
            Message: GetNewsletterError(results.error.ErrorMessage), 
            Icon: 2 
        };
    }
}
