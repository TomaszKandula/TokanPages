import { ItemDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ApplicationLanguageState } from "../../../../Store/States";

export interface BaseProperties extends ViewProperties {
    isAnonymous: boolean;
    drawerState: { open: boolean };
    openHandler: (event: any) => void;
    closeHandler: (event: any) => void;
    infoHandler: () => void;
    avatarName: string;
    avatarSource: string;
    userAliasText: string;
    logoImgName: string;
    menu: { image: string; items: ItemDto[] };
    backNavigationOnly?: boolean;
    backPathFragment?: string;
    languages: ApplicationLanguageState;
    languageId: string;
    languagePickHandler: (id: string) => void;
    languageMenuHandler: () => void;
    isLanguageMenuOpen: boolean;
}

export interface Properties extends BaseProperties {
    height?: number;
}

export interface LanguageSelectionProps {
    selection: string;
    languageId: string;
}
