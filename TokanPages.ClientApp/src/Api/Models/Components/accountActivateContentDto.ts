interface BaseData {
    type: string;
    caption: string;
    button: string;
}

interface BaseText {
    text1: string;
    text2: string;
}

interface TextData extends BaseData {
    text1: string;
    text2: string;
}

interface SuccessData extends BaseData {
    noBusinessLock: BaseText;
    businessLock: BaseText;
}

interface Fallback {
    caption: string;
    text: string[];
}

export interface AccountActivateContentDto {
    language: string;
    fallback: Fallback;
    onVerifying: TextData;
    onProcessing: TextData;
    onSuccess: SuccessData;
    onError: TextData;
}
