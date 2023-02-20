import { GetReadTimeInput } from "./interface";

export const GetReadTime = (props: GetReadTimeInput): string =>
{
    if (props.countWords === 0) return "0";
    let result: number = 0;
    result = props.countWords / props.wordsPerMinute;
    return result.toFixed(2);
}
