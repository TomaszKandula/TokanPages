import { useDispatch, useSelector } from "react-redux";
import { USER_DATA } from "../../Shared/constants";
import { GetDataFromStorage } from "./StorageServices";
import { UserDataStoreAction } from "../../Store/Actions";
import { ApplicationState } from "../../Store/Configuration";
import { AuthenticateUserResultDto } from "../../Api/Models";
import Validate from "validate.js";

export const UpdateUserData = (): void => 
{
    const dispatch = useDispatch();    
    const selector = useSelector((state: ApplicationState) => state.userDataStore);
    const data = GetDataFromStorage({ key: USER_DATA }) as AuthenticateUserResultDto;

    if (Object.entries(data).length !== 0 && Validate.isEmpty(selector?.userData?.userId))
    {
        dispatch(UserDataStoreAction.update(data));
    }
}
