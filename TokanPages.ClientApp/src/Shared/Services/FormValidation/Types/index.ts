interface ContentBaseProps {
    content: {
        emailInvalid?: string;
        nameInvalid?: string;
        surnameInvalid?: string;
        passwordInvalid?: string;
        missingTerms?: string;
        missingChar?: string;
        missingLargeLetter?: string;
        missingNumber?: string;
        missingSmallLetter?: string;
    };
}

interface NameBaseProps {
    firstName: string;
    lastName: string;
}

interface EmailBaseProps {
    email: string;
}

export interface ValidateAccountFormProps extends NameBaseProps, EmailBaseProps {
    description: string;
}

export interface ValidateBusinessFormProps extends NameBaseProps, EmailBaseProps {
    company: string;
    phone: string;
    description: string;
    techStack?: string[];
    services?: string[];
}

export interface ValidateContactFormProps extends NameBaseProps, EmailBaseProps {
    subject: string;
    message: string;
    terms: boolean;
}

export interface ValidateEmailFormProps extends EmailBaseProps {}

export interface ValidatePasswordFormProps extends ContentBaseProps {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
}

export interface ValidateResetFormProps extends EmailBaseProps {}

export interface ValidateSigninFormProps extends ContentBaseProps, EmailBaseProps {
    password: string;
}

export interface ValidateSignupFormProps extends ContentBaseProps, NameBaseProps, EmailBaseProps {
    password: string;
    terms: boolean;
}

export interface ValidateUpdateFormProps extends ContentBaseProps {
    newPassword: string;
    verifyPassword: string;
}
