import { PropsToFieldsInput } from "./interface";

export const PropsToFields = (props: PropsToFieldsInput): any[] =>
{
    let resultArray: any[] = [];
    for (let Property in props.object) resultArray = resultArray.concat(props.object[Property]); 
    return resultArray;
}
