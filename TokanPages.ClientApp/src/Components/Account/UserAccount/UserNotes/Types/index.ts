import { UserNoteResultDto } from "../../../../../Api/Models";
import { ReactChangeTextEvent } from "../../../../../Shared/Types";

export type ActionType = "ADD" | "UPDATE" | "REMOVE";

export interface UserNotesProps {
    background?: React.CSSProperties;
}

export interface UserNotesViewProps {
    isLoading: boolean;
    mediaQuery: {
        isTablet: boolean;
        isMobile: boolean;
    };
    hasProgress: boolean;
    userNotes?: UserNoteProps[];
    captionText: string;
    descriptionText: string;
    listLabel: string;
    noteLabel: string;
    onRowClick: (index: number) => void;
    selection?: UserNoteResultDto;
    clearButtonText: string;
    clearButtonHandler: () => void;
    removeButtonText: string;
    removeButtonHandler: () => void;
    saveButtonText: string;
    saveButtonHandler: () => void;
    messageForm: { note: string };
    messageHandler: (event: ReactChangeTextEvent) => void;
    background?: React.CSSProperties;
}

export interface UserNoteProps {
    id: string;
    note: string;
    createdBy: string;
    createdAt: string;
    modifiedBy?: string;
    modifiedAt?: string;
}

export interface RenderRowProps {
    id: string;
    note: string;
    index: number;
    selection?: string;
    onClick: (index: number) => void | undefined;
}
