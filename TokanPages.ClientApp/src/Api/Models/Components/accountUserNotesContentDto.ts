export interface AccountUserNotesContentDto {
    language: string;
    caption: string;
    description: string;
    listLabel: string;
    noteLabel: string;
	buttons: {
		removeText: string;
		saveText: string;
	}
}
