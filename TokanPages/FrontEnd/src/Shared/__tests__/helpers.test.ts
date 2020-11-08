import { IsEmpty } from "../helpers";

test("Given string is not mepty, expected IsEmpty: 'false'.", () => 
{
    expect( IsEmpty("string") ).toBe(false);
});
