import { LinkDto } from "./Common";

export interface TranslationItemProps {
    yearLabel: string;
    yearsLabel: string;
}

export interface TranslationsProps {
    aboveLabel: string;
    singular: TranslationItemProps;
    plural: TranslationItemProps;
}

export interface HeaderItem {
    fullName: string;
    mobilePhone: string;
    email: string;
    www: LinkDto;
    github: LinkDto;
}

export interface SummaryProps {
    caption: string;
    text: string;
}

export interface AchievementsProps {
    caption: string;
    list: string[];
}

export interface OccupationProps {
    name: string;
    dateStart: string;
    dateEnd: string;
    details: string[];
}

export interface InterestsProps {
    caption: string;
    list: string[];
}

export interface ExperienceItemProps {
    dateStart: string;
    dateEnd: string;
    companyName: string;
    contractType: string;
    occupation: OccupationProps[];
}

export interface ExperienceProps {
    caption: string;
    list: ExperienceItemProps[];
}

export interface ThesisProps {
    label: string;
    name: string;
    file: string;
}

export interface EducationItemProps {
    schoolName: string;
    tenureInfo: string;
    dateStart: string;
    dateEnd: string;
    details: string;
    thesis: ThesisProps;
}

export interface EducationProps {
    caption: string;
    list: EducationItemProps[];
}

export interface ResumeProps {
    header: HeaderItem;
    summary: SummaryProps;
    achievements: AchievementsProps;
    experience: ExperienceProps;
    education: EducationProps;
    interests: InterestsProps;
}

export interface ResumeContentDto {
    language: string;
    caption: string;
    photo: LinkDto;
    translations: TranslationsProps;
    resume: ResumeProps;
}
