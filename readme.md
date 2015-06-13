### MagicaVoxelUnity

Unity3D plugin for MagicaVoxel's "vox" format

#### Features
* Import "vox" files as meshes (faces combined if possible)
* Import "vox" files as individual voxels (GameObjects)
	* Voxels can be combined into a single GameObject, so you can cut the voxel model into several GameObjects (meshes) for animation purposes

#### How To
* Add MVVoxelModel script to your component 
	* Use editor controls to load "vox" files onto the object

* Use MVVoxelModel's member methods

		LoadVOXFile(filePath, importAsIndividualVoxels)
		LoadVOXData(data, importAsIndividualVoxels)

	* MVVoxelModel.sizePerVoxel and MVVoxelModel.voxMaterial can be configured before loading the file/data


	
#### License

MIT

#### Contact
* darkfall3 at gmail.com
* [http://darkfall.me](http://darkfall.me) 
