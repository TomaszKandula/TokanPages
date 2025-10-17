import { FigoureSize } from "../../../../Shared/Enums";

export interface UserAvatarProps {
    size: FigoureSize;
    userId?: string;
    userLetter?: string;
    avatarName?: string;
    altSource?: string;
    className?: string;
}
