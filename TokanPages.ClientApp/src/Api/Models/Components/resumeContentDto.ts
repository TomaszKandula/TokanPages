import { LinkDto } from "./Common";

interface TranslationItemProps {
    yearLabel: string;
    yearsLabel: string;
}

interface TranslationsProps {
    aboveLabel: string;
    singular: TranslationItemProps;
    plural: TranslationItemProps;
}

interface HeaderItem {
    fullName: string;
    mobilePhone: string;
    email: string;
    www: LinkDto;
}

interface SummaryProps {
    caption: string;
    text: string;
}

interface AchievementsProps {
    caption: string;
    list: string[];
}

interface OccupationProps {
    name: string;
    dateStart: string;
    dateEnd: string;
    details: string[];
}

interface InterestsProps {
    caption: string;
    list: string[];
}

interface ExperienceItemProps {
    dateStart: string;
    dateEnd: string;
    companyName: string;
    contractType: string;
    occupation: OccupationProps;
}

interface ExperienceProps {
    caption: string;
    list: ExperienceItemProps[];
}

interface ThesisProps {
    name: string;
    file: string;
}

interface EducationItemProps {
    schoolName: string;
    tenureInfo: string;
    dateStart: string;
    dateEnd: string;
    thesis: ThesisProps;
}

interface EducationProps {
    caption: string;
    list: EducationItemProps[];
}

interface ResumeProps {
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
