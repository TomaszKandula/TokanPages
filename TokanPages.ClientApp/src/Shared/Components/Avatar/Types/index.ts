import { FigoureSize } from "../../../../Shared/Enums";

export interface AvatarProps {
    alt: string;
    title: string;
    size: FigoureSize;
    src?: string;
    className?: string;
    children?: React.ReactElement | string;
}
