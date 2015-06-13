### MagicaVoxelUnity

Unity3D plugin for MagicaVoxel's "vox" format

#### Features
* Import "vox" files as meshes (faces combined if possible)
* Import "vox" files as individual voxels (GameObjects)
	* Voxels can be combined into a single GameObject, so you can cut the voxel model into several GameObjects (meshes) for animation purposes

#### How To
* Add MVVoxelModel script to your component 
	* Use editor controls to load "vox" files onto the object

	* Or use MVVoxelModel's member methods

			LoadVOXFile(filePath, importAsIndividualVoxels)
			LoadVOXData(data, importAsIndividualVoxels)

			MVVoxelModel.sizePerVoxel and MVVoxelModel.voxMaterial can be configured before loading the file/data
			
* When imported as separate voxels, selecting multiple voxels in the editor will give you a control to combine the selected voxels into a single mesh
	* The mesh can be divided into separate voxels again
			
* Use static methods in MVImporter

		Load main chunk:
		
			LoadVOX(file)
			LoadVOXFromData(data)
			
		Create meshes from main chunk:
		
			CreateMeshes(mainChunk, sizePerVox)
			
		Or create meshes and GameObjects from main chunk:
		
			CreateVoxelGameObjects(mainChunk, parentTransform, material, sizePerVox)
			CreateIndividualVoxelGameObjects(mainChunk, parentTransform, material, sizePerVox)
		
	
#### License

MIT

#### Contact
* darkfall3 at gmail.com
* [http://darkfall.me](http://darkfall.me) 
