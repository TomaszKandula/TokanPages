import { TwitterModelDto } from "./twitterModelDto";

export interface FacebookModelDto extends TwitterModelDto {
    imageAlt: string;
}
