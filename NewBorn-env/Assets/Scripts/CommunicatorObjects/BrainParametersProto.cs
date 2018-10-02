// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: communicator_objects/brain_parameters_proto.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MLAgents.CommunicatorObjects {

  /// <summary>Holder for reflection information generated from communicator_objects/brain_parameters_proto.proto</summary>
  public static partial class BrainParametersProtoReflection {

    #region Descriptor
    /// <summary>File descriptor for communicator_objects/brain_parameters_proto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BrainParametersProtoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjFjb21tdW5pY2F0b3Jfb2JqZWN0cy9icmFpbl9wYXJhbWV0ZXJzX3Byb3Rv",
            "LnByb3RvEhRjb21tdW5pY2F0b3Jfb2JqZWN0cxorY29tbXVuaWNhdG9yX29i",
            "amVjdHMvcmVzb2x1dGlvbl9wcm90by5wcm90bxorY29tbXVuaWNhdG9yX29i",
            "amVjdHMvYnJhaW5fdHlwZV9wcm90by5wcm90bxorY29tbXVuaWNhdG9yX29i",
            "amVjdHMvc3BhY2VfdHlwZV9wcm90by5wcm90byLGAwoUQnJhaW5QYXJhbWV0",
            "ZXJzUHJvdG8SHwoXdmVjdG9yX29ic2VydmF0aW9uX3NpemUYASABKAUSJwof",
            "bnVtX3N0YWNrZWRfdmVjdG9yX29ic2VydmF0aW9ucxgCIAEoBRIaChJ2ZWN0",
            "b3JfYWN0aW9uX3NpemUYAyABKAUSQQoSY2FtZXJhX3Jlc29sdXRpb25zGAQg",
            "AygLMiUuY29tbXVuaWNhdG9yX29iamVjdHMuUmVzb2x1dGlvblByb3RvEiIK",
            "GnZlY3Rvcl9hY3Rpb25fZGVzY3JpcHRpb25zGAUgAygJEkYKGHZlY3Rvcl9h",
            "Y3Rpb25fc3BhY2VfdHlwZRgGIAEoDjIkLmNvbW11bmljYXRvcl9vYmplY3Rz",
            "LlNwYWNlVHlwZVByb3RvEksKHXZlY3Rvcl9vYnNlcnZhdGlvbl9zcGFjZV90",
            "eXBlGAcgASgOMiQuY29tbXVuaWNhdG9yX29iamVjdHMuU3BhY2VUeXBlUHJv",
            "dG8SEgoKYnJhaW5fbmFtZRgIIAEoCRI4CgpicmFpbl90eXBlGAkgASgOMiQu",
            "Y29tbXVuaWNhdG9yX29iamVjdHMuQnJhaW5UeXBlUHJvdG9CH6oCHE1MQWdl",
            "bnRzLkNvbW11bmljYXRvck9iamVjdHNiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::MLAgents.CommunicatorObjects.ResolutionProtoReflection.Descriptor, global::MLAgents.CommunicatorObjects.BrainTypeProtoReflection.Descriptor, global::MLAgents.CommunicatorObjects.SpaceTypeProtoReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MLAgents.CommunicatorObjects.BrainParametersProto), global::MLAgents.CommunicatorObjects.BrainParametersProto.Parser, new[]{ "VectorObservationSize", "NumStackedVectorObservations", "VectorActionSize", "CameraResolutions", "VectorActionDescriptions", "VectorActionSpaceType", "VectorObservationSpaceType", "BrainName", "BrainType" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class BrainParametersProto : pb::IMessage<BrainParametersProto> {
    private static readonly pb::MessageParser<BrainParametersProto> _parser = new pb::MessageParser<BrainParametersProto>(() => new BrainParametersProto());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BrainParametersProto> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MLAgents.CommunicatorObjects.BrainParametersProtoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BrainParametersProto() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BrainParametersProto(BrainParametersProto other) : this() {
      vectorObservationSize_ = other.vectorObservationSize_;
      numStackedVectorObservations_ = other.numStackedVectorObservations_;
      vectorActionSize_ = other.vectorActionSize_;
      cameraResolutions_ = other.cameraResolutions_.Clone();
      vectorActionDescriptions_ = other.vectorActionDescriptions_.Clone();
      vectorActionSpaceType_ = other.vectorActionSpaceType_;
      vectorObservationSpaceType_ = other.vectorObservationSpaceType_;
      brainName_ = other.brainName_;
      brainType_ = other.brainType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BrainParametersProto Clone() {
      return new BrainParametersProto(this);
    }

    /// <summary>Field number for the "vector_observation_size" field.</summary>
    public const int VectorObservationSizeFieldNumber = 1;
    private int vectorObservationSize_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int VectorObservationSize {
      get { return vectorObservationSize_; }
      set {
        vectorObservationSize_ = value;
      }
    }

    /// <summary>Field number for the "num_stacked_vector_observations" field.</summary>
    public const int NumStackedVectorObservationsFieldNumber = 2;
    private int numStackedVectorObservations_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int NumStackedVectorObservations {
      get { return numStackedVectorObservations_; }
      set {
        numStackedVectorObservations_ = value;
      }
    }

    /// <summary>Field number for the "vector_action_size" field.</summary>
    public const int VectorActionSizeFieldNumber = 3;
    private int vectorActionSize_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int VectorActionSize {
      get { return vectorActionSize_; }
      set {
        vectorActionSize_ = value;
      }
    }

    /// <summary>Field number for the "camera_resolutions" field.</summary>
    public const int CameraResolutionsFieldNumber = 4;
    private static readonly pb::FieldCodec<global::MLAgents.CommunicatorObjects.ResolutionProto> _repeated_cameraResolutions_codec
        = pb::FieldCodec.ForMessage(34, global::MLAgents.CommunicatorObjects.ResolutionProto.Parser);
    private readonly pbc::RepeatedField<global::MLAgents.CommunicatorObjects.ResolutionProto> cameraResolutions_ = new pbc::RepeatedField<global::MLAgents.CommunicatorObjects.ResolutionProto>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MLAgents.CommunicatorObjects.ResolutionProto> CameraResolutions {
      get { return cameraResolutions_; }
    }

    /// <summary>Field number for the "vector_action_descriptions" field.</summary>
    public const int VectorActionDescriptionsFieldNumber = 5;
    private static readonly pb::FieldCodec<string> _repeated_vectorActionDescriptions_codec
        = pb::FieldCodec.ForString(42);
    private readonly pbc::RepeatedField<string> vectorActionDescriptions_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> VectorActionDescriptions {
      get { return vectorActionDescriptions_; }
    }

    /// <summary>Field number for the "vector_action_space_type" field.</summary>
    public const int VectorActionSpaceTypeFieldNumber = 6;
    private global::MLAgents.CommunicatorObjects.SpaceTypeProto vectorActionSpaceType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MLAgents.CommunicatorObjects.SpaceTypeProto VectorActionSpaceType {
      get { return vectorActionSpaceType_; }
      set {
        vectorActionSpaceType_ = value;
      }
    }

    /// <summary>Field number for the "vector_observation_space_type" field.</summary>
    public const int VectorObservationSpaceTypeFieldNumber = 7;
    private global::MLAgents.CommunicatorObjects.SpaceTypeProto vectorObservationSpaceType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MLAgents.CommunicatorObjects.SpaceTypeProto VectorObservationSpaceType {
      get { return vectorObservationSpaceType_; }
      set {
        vectorObservationSpaceType_ = value;
      }
    }

    /// <summary>Field number for the "brain_name" field.</summary>
    public const int BrainNameFieldNumber = 8;
    private string brainName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string BrainName {
      get { return brainName_; }
      set {
        brainName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "brain_type" field.</summary>
    public const int BrainTypeFieldNumber = 9;
    private global::MLAgents.CommunicatorObjects.BrainTypeProto brainType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MLAgents.CommunicatorObjects.BrainTypeProto BrainType {
      get { return brainType_; }
      set {
        brainType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BrainParametersProto);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BrainParametersProto other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (VectorObservationSize != other.VectorObservationSize) return false;
      if (NumStackedVectorObservations != other.NumStackedVectorObservations) return false;
      if (VectorActionSize != other.VectorActionSize) return false;
      if(!cameraResolutions_.Equals(other.cameraResolutions_)) return false;
      if(!vectorActionDescriptions_.Equals(other.vectorActionDescriptions_)) return false;
      if (VectorActionSpaceType != other.VectorActionSpaceType) return false;
      if (VectorObservationSpaceType != other.VectorObservationSpaceType) return false;
      if (BrainName != other.BrainName) return false;
      if (BrainType != other.BrainType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (VectorObservationSize != 0) hash ^= VectorObservationSize.GetHashCode();
      if (NumStackedVectorObservations != 0) hash ^= NumStackedVectorObservations.GetHashCode();
      if (VectorActionSize != 0) hash ^= VectorActionSize.GetHashCode();
      hash ^= cameraResolutions_.GetHashCode();
      hash ^= vectorActionDescriptions_.GetHashCode();
      if (VectorActionSpaceType != 0) hash ^= VectorActionSpaceType.GetHashCode();
      if (VectorObservationSpaceType != 0) hash ^= VectorObservationSpaceType.GetHashCode();
      if (BrainName.Length != 0) hash ^= BrainName.GetHashCode();
      if (BrainType != 0) hash ^= BrainType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (VectorObservationSize != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(VectorObservationSize);
      }
      if (NumStackedVectorObservations != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(NumStackedVectorObservations);
      }
      if (VectorActionSize != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(VectorActionSize);
      }
      cameraResolutions_.WriteTo(output, _repeated_cameraResolutions_codec);
      vectorActionDescriptions_.WriteTo(output, _repeated_vectorActionDescriptions_codec);
      if (VectorActionSpaceType != 0) {
        output.WriteRawTag(48);
        output.WriteEnum((int) VectorActionSpaceType);
      }
      if (VectorObservationSpaceType != 0) {
        output.WriteRawTag(56);
        output.WriteEnum((int) VectorObservationSpaceType);
      }
      if (BrainName.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(BrainName);
      }
      if (BrainType != 0) {
        output.WriteRawTag(72);
        output.WriteEnum((int) BrainType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (VectorObservationSize != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(VectorObservationSize);
      }
      if (NumStackedVectorObservations != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(NumStackedVectorObservations);
      }
      if (VectorActionSize != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(VectorActionSize);
      }
      size += cameraResolutions_.CalculateSize(_repeated_cameraResolutions_codec);
      size += vectorActionDescriptions_.CalculateSize(_repeated_vectorActionDescriptions_codec);
      if (VectorActionSpaceType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) VectorActionSpaceType);
      }
      if (VectorObservationSpaceType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) VectorObservationSpaceType);
      }
      if (BrainName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BrainName);
      }
      if (BrainType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) BrainType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BrainParametersProto other) {
      if (other == null) {
        return;
      }
      if (other.VectorObservationSize != 0) {
        VectorObservationSize = other.VectorObservationSize;
      }
      if (other.NumStackedVectorObservations != 0) {
        NumStackedVectorObservations = other.NumStackedVectorObservations;
      }
      if (other.VectorActionSize != 0) {
        VectorActionSize = other.VectorActionSize;
      }
      cameraResolutions_.Add(other.cameraResolutions_);
      vectorActionDescriptions_.Add(other.vectorActionDescriptions_);
      if (other.VectorActionSpaceType != 0) {
        VectorActionSpaceType = other.VectorActionSpaceType;
      }
      if (other.VectorObservationSpaceType != 0) {
        VectorObservationSpaceType = other.VectorObservationSpaceType;
      }
      if (other.BrainName.Length != 0) {
        BrainName = other.BrainName;
      }
      if (other.BrainType != 0) {
        BrainType = other.BrainType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            VectorObservationSize = input.ReadInt32();
            break;
          }
          case 16: {
            NumStackedVectorObservations = input.ReadInt32();
            break;
          }
          case 24: {
            VectorActionSize = input.ReadInt32();
            break;
          }
          case 34: {
            cameraResolutions_.AddEntriesFrom(input, _repeated_cameraResolutions_codec);
            break;
          }
          case 42: {
            vectorActionDescriptions_.AddEntriesFrom(input, _repeated_vectorActionDescriptions_codec);
            break;
          }
          case 48: {
            vectorActionSpaceType_ = (global::MLAgents.CommunicatorObjects.SpaceTypeProto) input.ReadEnum();
            break;
          }
          case 56: {
            vectorObservationSpaceType_ = (global::MLAgents.CommunicatorObjects.SpaceTypeProto) input.ReadEnum();
            break;
          }
          case 66: {
            BrainName = input.ReadString();
            break;
          }
          case 72: {
            brainType_ = (global::MLAgents.CommunicatorObjects.BrainTypeProto) input.ReadEnum();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
