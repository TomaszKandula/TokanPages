import { ItemDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ApplicationLanguageState } from "../../../../Store/States";

export interface BaseProperties extends ViewProperties {
    drawerState: { open: boolean };
    openHandler: (event: any) => void;
    closeHandler: (event: any) => void;
    infoHandler: () => void;
    isAnonymous: boolean;
    avatarName: string;
    avatarSource: string;
    userAliasText: string;
    logoImgName: string;
    languages: ApplicationLanguageState;
    languageId: string;
    languageHandler: (id: string) => void;
    menu: { image: string; items: ItemDto[] };
    backNavigationOnly?: boolean;
    backPathFragment?: string;
}

export interface Properties extends BaseProperties {
    height?: number;
}

export interface LanguageSelectionProps {
    selection: string;
    languageId: string;
}
