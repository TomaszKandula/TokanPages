import { LinkDto, WarningPasswordDto } from "./Common";

export interface AccountUserSignupContentDto {
    language: string;
    caption: string;
    button: string;
    link: LinkDto;
    warning: WarningPasswordDto;
    consent: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}
