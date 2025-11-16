export type MediaType = "image" | "video";

export interface MediaPresenterProps {
    isOpen: boolean;
    autoplay?: boolean;
    background?: string;
    isNavigationOff?: boolean;
    presenting: number;
    collection: string[];
    posters: string[];
    type: MediaType;
    onTrigger: () => void;
}
