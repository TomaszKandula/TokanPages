import { IPropsToFields } from "./interface";

export const PropsToFields = (props: IPropsToFields): any[] =>
{
    let resultArray: any[] = [];
    for (let Property in props.object) resultArray = resultArray.concat(props.object[Property]); 
    return resultArray;
}
