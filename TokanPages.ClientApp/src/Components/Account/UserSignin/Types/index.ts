import { LinkDto, NewsItemDto } from "../../../../Api/Models";
import { ReactChangeEvent, ReactKeyboardEvent, ViewProperties } from "../../../../Shared/Types";

export interface UserSigninProps {
    className?: string;
}

export interface UserSigninViewProps extends ViewProperties, UserSigninProps {
    caption: string;
    button: string;
    link1: LinkDto;
    link2: LinkDto;
    consent: string;
    security: NewsItemDto[];
    buttonHandler: () => void;
    progress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    password: string;
    labelEmail: string;
    labelPassword: string;
}

export interface RenderSigninCardProps extends UserSigninViewProps {
    className?: string;
}

export interface RenderSliderProps extends UserSigninViewProps {
    className?: string;
}

export interface RenderSlideProps {
    image: string;
    tags: string[];
    date: string;
    title: string;
    lead: string;
    isLoading?: boolean;
}
