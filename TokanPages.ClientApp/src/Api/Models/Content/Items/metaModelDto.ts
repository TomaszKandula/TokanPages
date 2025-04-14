import { FacebookModelDto } from "./facebookModelDto";
import { TwitterModelDto } from "./twitterModelDto";

export interface MetaModelDto {
    language: string;
    description: string;
    facebook: FacebookModelDto;
    twitter: TwitterModelDto;
}
