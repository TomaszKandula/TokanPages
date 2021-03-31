import { apiRequest } from "../requests";
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
    const request = apiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_ADD_SUBSCRIBER, 
        data: 
        { 
            email: PayLoad.email 
        } 
    });

    const results = await request;
    return results.isSucceeded ? { 
        State: true, 
        Title: "Newsletter", 
        Message: GetNewsletterSuccess(), 
        Icon: 0 
    } : { 
        State: true, 
        Title: "Newsletter | Error", 
        Message: GetNewsletterError(results.error.ErrorMessage), 
        Icon: 2 
    };
}

export const UpdateSubscriberData = async (PayLoad: IUpdateSubscriberDto): Promise<IAlertModal> =>
{
    const request = apiRequest(
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

    const results = await request;
    return results.isSucceeded ? { 
        State: true, 
        Title: "Subscriber email update", 
        Message: GetNewsletterSuccess(), 
        Icon: 0 
    } : { 
        State: true, 
        Title: "Subscriber email update | Error", 
        Message: GetNewsletterError(results.error.ErrorMessage), 
        Icon: 2 
    };
}

export const RemoveSubscriberData = async (PayLoad: IRemoveSubscriberDto): Promise<IAlertModal> =>
{
    const request = apiRequest(
    { 
        method: "POST", 
        url: API_COMMAND_REMOVE_SUBSCRIBER, 
        data: 
        { 
            id: PayLoad.id
        } 
    });

    const results = await request;
    return results.isSucceeded ? { 
        State: true, 
        Title: "Unsubscribe", 
        Message: GetNewsletterSuccess(), 
        Icon: 0 
    } : { 
        State: true, 
        Title: "Unsubscribe | Error", 
        Message: GetNewsletterError(results.error.ErrorMessage), 
        Icon: 2 
    };
}
