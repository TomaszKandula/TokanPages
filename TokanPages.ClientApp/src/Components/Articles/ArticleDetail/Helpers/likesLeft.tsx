import { ArticlesProps } from "../../../../Api/Models";

export const LikesLeft = (isAnonymous: boolean, likesLeft: number, template: ArticlesProps) => {
    const textLikesLeft = isAnonymous
        ? template.likesHintAnonym.replace("{LEFT_LIKES}", likesLeft.toString())
        : template.likesHintUser.replace("{LEFT_LIKES}", likesLeft.toString());

    return likesLeft === 0 ? template.maxLikesReached : textLikesLeft;
};
