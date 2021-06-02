import { LIKES_HINT_FOR_ANONYM, LIKES_HINT_FOR_USER, MAX_LIKES_REACHED } from "../../../Shared/constants";

export const LikesLeft = (isAnonymous: boolean, likesLeft: number) =>
{
    const textLikesLeft = isAnonymous 
        ? LIKES_HINT_FOR_ANONYM.replace("{LEFT_LIKES}", likesLeft.toString()) 
        : LIKES_HINT_FOR_USER.replace("{LEFT_LIKES}", likesLeft.toString());

    return likesLeft === 0 
        ? MAX_LIKES_REACHED 
        : textLikesLeft;
};
