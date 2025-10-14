import { IconDto, LinkDto } from "../../../../Api/Models";

export interface LoaderProps {
    isLoading: boolean;
    children: React.ReactElement;
}

export interface LegalInfoProps {
    copyright: string;
    reserved: string;
}

export interface FooterViewProps {
    isLoading: boolean;
    terms: LinkDto;
    policy: LinkDto;
    versionInfo: string;
    hasVersionInfo: boolean;
    legalInfo: LegalInfoProps;
    hasLegalInfo: boolean;
    icons: IconDto[];
    hasIcons: boolean;
}
