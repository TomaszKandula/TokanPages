import { UserSignupContentDto } from "../../../Api/Models";

export interface ContentUserSignupState extends UserSignupContentDto {
    isLoading: boolean;
}
