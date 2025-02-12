import { LinkDto } from "./Common";

export interface AccountUserSignupContentDto {
    language: string;
    caption: string;
    button: string;
    link: LinkDto;
    warning: string;
    consent: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}
