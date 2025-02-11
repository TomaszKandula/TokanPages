export interface AccountUserSigninContentDto {
    language: string;
    caption: string;
    button: string;
    link1: LinkPropsDto;
    link2: LinkPropsDto;
    labelEmail: string;
    labelPassword: string;
}

export interface LinkPropsDto {
    text: string;
    href: string;
}
