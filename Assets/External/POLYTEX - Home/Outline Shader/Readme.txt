This outline solution doesnt work for complex meshes but it is good 
for giving suttle outline effect for simple meshes.

It works by creating a duplicate object then scaling it from objects pivot point 
and then rendering the object twice. Back faces for the outline object and the 
front faces for the main material. When scaling complex objects the back faces 
come trough the mesh and it doesnt work properly.

This shader doesnt work correctly when Static Batching for gameobject is enabled.
Static Batching unites meshes as one and then projects the outline effect from united mesh data.

To use Outline Shader you need to follow these steps:
	- Select the object that you want to have the outline
	- Attach the OutlineScript to the object.
	- Place the Ouline_Mat1 to the Outline Material slot.
	- Outline Scale Factor needs to be over 1 to work properly. I use around 1.01-1.05
	- Choose Outline Color.

This shader only works properly when the objects scale is set to 1 on all axis.