﻿using System.Collections.Generic;
using System.Linq;
using Dargon.IO;

namespace Dargon.RADS.Manifest {
   public class ReleaseManifestRootDirectoryOverload : IReleaseManifestDirectoryEntry, ReadableDargonNode {
      private readonly ReleaseManifestDirectoryEntry directory;
      private readonly string name;

      public ReleaseManifestRootDirectoryOverload(ReleaseManifestDirectoryEntry directory, string name) {
         this.directory = directory;
         this.name = name;

         foreach (var subdirectory in directory.m_directories) {
            subdirectory.m_parent = this;
         }

         foreach (var file in directory.m_files) {
            file.m_parent = this;
         }
      }

      // IReadableDargonNode Impl: 
      public string Name { get { return name; } }
      ReadableDargonNode ReadableDargonNode.Parent { get { return null; } }
      public IReadOnlyCollection<ReadableDargonNode> Children { get { return directory.Children; } }
      public T GetComponentOrNull<T>() { return directory.GetComponentOrNull<T>(); }
      public bool TryGetChild(string name, out ReadableDargonNode child) {
         child = Children.FirstOrDefault(x => x.NameEquals(name));
         return child != null;
      }

      // IReleaseManifestEntry Impl: 
      public IReleaseManifestDirectoryEntry Parent { get { return null; } }
      public IReadOnlyCollection<ReleaseManifestFileEntry> Files { get { return directory.Files; } }
      public IReadOnlyCollection<IReleaseManifestDirectoryEntry> Directories { get { return directory.Directories; } }

      public uint DirectoryId { get { return directory.DirectoryId; } }
      public uint NameStringTableIndex { get { return directory.NameStringTableIndex; } }
      public uint SubdirectoryStart { get { return directory.SubdirectoryStart; } }
      public uint SubdirectoryCount { get { return directory.SubdirectoryCount; } }
      public uint FileStart { get { return directory.FileStart; } }
      public uint FileCount { get { return directory.FileCount; } }
   }
}
