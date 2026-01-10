export interface AccountUserFilesContentDto {
    language: string;
    caption: string;
    description: string;
    listLabel: string;
    buttons: {
        removeText: string;
        clearText: string;
        saveText: string;
    };
}
