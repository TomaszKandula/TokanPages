import { AuthenticateUserResultDto } from "../../../Api/Models";

export interface UserDataStoreState {
    isShown: boolean;
    userData: AuthenticateUserResultDto;
}
