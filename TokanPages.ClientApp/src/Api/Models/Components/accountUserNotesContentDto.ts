export interface AccountUserNotesContentDto {
    language: string;
    caption: string;
    header: string;
	buttons: {
		removeText: string;
		saveText: string;
	}
}
