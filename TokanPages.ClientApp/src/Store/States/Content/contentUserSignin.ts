import { UserSigninContentDto } from "../../../Api/Models";

export interface ContentUserSigninState extends UserSigninContentDto {
    isLoading: boolean;
}
