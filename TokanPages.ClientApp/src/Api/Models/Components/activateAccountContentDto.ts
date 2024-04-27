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

export interface ActivateAccountContentDto {
    content: {
        language: string;
        onVerifying: TextData;
        onProcessing: TextData;
        onSuccess: SuccessData;
        onError: TextData;
    };
}
