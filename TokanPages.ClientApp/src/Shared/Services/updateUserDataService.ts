import { useDispatch, useSelector } from "react-redux";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import { ActionCreators } from "../../Redux/Actions/Users/storeUserDataAction";
import { IApplicationState } from "../../Redux/applicationState";
import { IAuthenticateUserResultDto } from "../../Api/Models";
import Validate from "validate.js";

export const StoreUserData = (): void => 
{
    const dispatch = useDispatch();    
    const selector = useSelector((state: IApplicationState) => state.storeUserData);
    const data = GetDataFromStorage({ key: USER_DATA }) as IAuthenticateUserResultDto;

    if (Object.entries(data).length !== 0 && Validate.isEmpty(selector?.userData?.userId))
    {
        dispatch(ActionCreators.update(data));
    }
}
